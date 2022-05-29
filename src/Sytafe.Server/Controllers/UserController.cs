using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Sytafe.Library.Extensions;
using Sytafe.Library.Models;
using Sytafe.Server.Data;
using Sytafe.Server.Models;

namespace Sytafe.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserController> _logger;
    private readonly AppSettings _settings;

    public UserController(AppDbContext context, ILogger<UserController> logger, IOptions<AppSettings> settings)
    {
        _context = context;
        _logger = logger;
        _settings = settings.Value;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserInfo>>> Get()
    {
        try
        {
            return await _context.Users.Select(x => x.ToInfo()).ToListAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserInfo>> Get(string id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return BadRequest(new ExceptionInfo("That user doesn't exist."));
            }
            return user.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserInfo>> Post([FromBody] UserInfo value)
    {
        try
        {
            var message = value.Validate();
            if (!string.IsNullOrEmpty(message))
            {
                return BadRequest(new ExceptionInfo(message));
            }
            var user = new UserInfo
            {
                Password = value.Password.ToSHA512(),
                Type = value.Type ?? string.Empty,
                Username = value.Username,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [AllowAnonymous]
    [HttpPost("Sign-In")]
    public async Task<ActionResult<UserInfo>> PostForSignIn([FromBody] UserInfo value)
    {
        try
        {
            var user = await _context.Users.Include(x => x.ScreenTimes).Include(x => x.Useds).FirstOrDefaultAsync(x => x.Username == value.Username && x.Password == value.Password.ToSHA512());
            if (user is null)
            {
                return Unauthorized(new ExceptionInfo("Your account or password is incorrect."));
            }

            var jwt = _settings.Jwt;

            var audience = jwt.Audience;
            var claims = new List<Claim>
            {
                new Claim("jti", user.ToJson()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var issuer = jwt.Issuer;
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddDays(1), signingCredentials: signingCredentials);

            user.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return user.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserInfo>> Put([FromBody] UserInfo value, string id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return BadRequest(new ExceptionInfo("That user doesn't exist."));
            }
            user.Type = value.Type;
            user.Username = value.Username;
            if (!string.IsNullOrEmpty(value.Password))
            {
                user.Password = value.Password.ToSHA512();
            }
            await _context.SaveChangesAsync();
            return user.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var user = await _context.Users.Include(x => x.ScreenTimes).FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return BadRequest(new ExceptionInfo("That user doesn't exist."));
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sytafe.Library.Extensions;
using Sytafe.Library.Models;
using Sytafe.Server.Data;

namespace Sytafe.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsedController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<UsedController> _logger;

    public UsedController(AppDbContext context, ILogger<UsedController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("Today/Using/User/{userId}")]
    public async Task<ActionResult<UsedInfo>> GetForTodayForUsingByUser(string userId)
    {
        try
        {
            var used = await _context.Useds.FirstOrDefaultAsync(x => x.From.Date == DateTime.Now.Date && x.To == DateTime.MinValue && x.UserId == userId);
            if (used is null)
            {
                return NoContent();
            }
            return used.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpPost("Today")]
    public async Task<ActionResult<UsedInfo>> PostForToday([FromBody] UsedInfo value)
    {
        try
        {
            var used = new UsedInfo
            {
                DayOfWeek = value.DayOfWeek,
                From = DateTime.Now,
                UserId = value.UserId,
            };
            await _context.Useds.AddAsync(used);
            await _context.SaveChangesAsync();
            return used.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpPut("Today/{id}")]
    public async Task<ActionResult<UsedInfo>> PutForToday(string id)
    {
        try
        {
            var used = await _context.Useds.FirstOrDefaultAsync(x => x.Id == id);
            if (used is null)
            {
                return BadRequest(new ExceptionInfo("That used doesn't exist."));
            }
            used.To = DateTime.Now;
            await _context.SaveChangesAsync();
            return used.ToInfo();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }
}
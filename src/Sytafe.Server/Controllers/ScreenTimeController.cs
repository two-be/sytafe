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
public class ScreenTimeController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<ScreenTimeController> _logger;

    public ScreenTimeController(AppDbContext context, ILogger<ScreenTimeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("User/{userId}")]
    public async Task<ActionResult<List<ScreenTimeInfo>>> GetByUser(string userId)
    {
        try
        {
            var screenTimes = await _context.ScreenTimes.Where(x => x.UserId == userId).Select(x => x.ToInfo()).ToListAsync();
            return screenTimes;
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }

    [HttpPost]
    public async Task<ActionResult<List<ScreenTimeInfo>>> Post([FromBody] List<ScreenTimeInfo> value)
    {
        try
        {
            var screenTimes = value.Select(x =>
            {
                var screenTime = _context.ScreenTimes.FirstOrDefault(y => y.DayOfWeek == x.DayOfWeek && y.UserId == x.UserId);
                if (screenTime is null) {
                    screenTime = new ScreenTimeInfo();
                    _context.ScreenTimes.Add(screenTime);
                }
                screenTime.Anytime = x.Anytime;
                screenTime.AvailableFrom = x.AvailableFrom;
                screenTime.AvailableTo = x.AvailableTo;
                screenTime.DayOfWeek = x.DayOfWeek;
                screenTime.MinuteLimit = x.MinuteLimit;
                screenTime.UserId = x.UserId;
                return screenTime;
            }).ToList();
            await _context.SaveChangesAsync();
            return screenTimes;
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToInfo());
        }
    }
}
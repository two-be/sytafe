using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Sytafe.Server.Data;

namespace Sytafe.Server.Hubs;

public class AppHub : Hub
{
    private readonly AppDbContext _context;

    public AppHub(AppDbContext context)
    {
        _context = context;
    }

    public void Used(string usedId)
    {
        Context.Items.Add("used", usedId);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var usedId = Context.Items["used"];
        var used = await _context.Useds.FirstOrDefaultAsync(x => x.Id == usedId);
        if (used is not null)
        {
            used.To = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        await base.OnDisconnectedAsync(exception);
    }
}
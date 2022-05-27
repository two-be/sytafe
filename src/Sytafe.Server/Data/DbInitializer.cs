using Microsoft.EntityFrameworkCore;
using Sytafe.Library.Models;
using Sytafe.Server.Utilities;

namespace Sytafe.Server.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        if (!Directory.Exists("../database"))
        {
            Directory.CreateDirectory("../database");
        }
        // context.Database.Migrate();
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.Add(new UserInfo
            {
                Password = "6AD275D26C200E81534D9996183C8748DDFABC7B0A011A90F46301626D709923474703CACAB0FF8B67CD846B6CB55B23A39B03FBDFB5218EEC3373CF7010A166",
                Type = UserType.Administrator,
                Username = "Two",
            });
        }

        context.SaveChanges();
    }
}
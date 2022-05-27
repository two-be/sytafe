#nullable disable

using Microsoft.EntityFrameworkCore;
using Sytafe.Library.Models;

namespace Sytafe.Server.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ScreenTimeInfo> ScreenTimes { get; set; }
    public DbSet<UsedInfo> Useds { get; set; }
    public DbSet<UserInfo> Users { get; set; }
}
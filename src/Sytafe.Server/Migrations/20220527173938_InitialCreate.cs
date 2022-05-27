using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sytafe.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenTimes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Anytime = table.Column<bool>(type: "INTEGER", nullable: false),
                    AvailableFrom = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    AvailableTo = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    DayOfWeek = table.Column<string>(type: "TEXT", nullable: true),
                    MinuteLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreenTimes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Useds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    From = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DayOfWeek = table.Column<string>(type: "TEXT", nullable: true),
                    To = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Useds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Useds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScreenTimes_UserId",
                table: "ScreenTimes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Useds_UserId",
                table: "Useds",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreenTimes");

            migrationBuilder.DropTable(
                name: "Useds");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

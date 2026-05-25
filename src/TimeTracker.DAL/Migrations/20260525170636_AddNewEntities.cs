using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ExpectedWork = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Worked = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Missing = table.Column<TimeSpan>(type: "interval", nullable: false),
                    LateCount = table.Column<int>(type: "integer", nullable: false),
                    EarlyLeaveCount = table.Column<int>(type: "integer", nullable: false),
                    LateWithReason = table.Column<int>(type: "integer", nullable: false),
                    LateWithoutReason = table.Column<int>(type: "integer", nullable: false),
                    EarlyWithReason = table.Column<int>(type: "integer", nullable: false),
                    EarlyWithoutReason = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStatistics");
        }
    }
}

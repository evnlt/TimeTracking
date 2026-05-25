using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // USERS
            migrationBuilder.Sql("""
                                    INSERT INTO "Users" ("Id")
                                    VALUES (1), (2), (3);
                                 """);

            // CARDS
            migrationBuilder.Sql("""
                                     INSERT INTO "Cards" ("CardUid", "UserId", "AssignedAt")
                                     VALUES
                                         ('001', 1, NOW()),
                                         ('002', 2, NOW()),
                                         ('003', 3, NOW());
                                 """);

            // WORK SCHEDULES
            migrationBuilder.Sql("""
                                     INSERT INTO "WorkSchedules" ("UserId", "StartTime", "EndTime", "FreeSchedule", "WorkingDays")
                                     VALUES
                                         (1, '09:00:00', '18:00:00', false, '["Monday","Tuesday","Wednesday","Thursday","Friday"]'),
                                         (2, '10:00:00', '19:00:00', false, '["Monday","Tuesday","Wednesday","Thursday","Friday"]'),
                                         (3, '00:00:00', '00:00:00', true, '[]');
                                 """);

            // ATTENDANCE RECORDS
            migrationBuilder.Sql("""
                                     INSERT INTO "AttendanceRecords" ("Id", "UserId", "AttendanceDate", "CheckIn", "CheckOut")
                                     VALUES
                                         (1, 1, '2026-05-24', '2026-05-24 09:05:00', '2026-05-24 18:10:00'),
                                         (2, 2, '2026-05-24', '2026-05-24 10:02:00', '2026-05-24 19:00:00'),
                                         (3, 3, '2026-05-24', '2026-05-24 08:50:00', NULL);
                                 """);

            // SCHEDULE EXCLUSIONS
            migrationBuilder.Sql("""
                                    INSERT INTO "ScheduleExclusions" ("Id", "UserId", "Type", "StartDateTime", "EndDateTime")
                                    VALUES
                                        (1, 1, 0, '2026-05-20 00:00:00', '2026-05-21 23:59:59'), -- vacation   
                                        (2, 2, 1, '2026-05-24 00:00:00', '2026-05-24 12:00:00'); -- late arrival allowed
                                 """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CardEntity> Cards => Set<CardEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<AttendanceRecordEntity> AttendanceRecords => Set<AttendanceRecordEntity>();
    public DbSet<WorkScheduleEntity> WorkSchedules => Set<WorkScheduleEntity>();
    public DbSet<ScheduleExclusionEntity> ScheduleExclusions => Set<ScheduleExclusionEntity>();
    public DbSet<UserStatisticsEntity> UserStatistics => Set<UserStatisticsEntity>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
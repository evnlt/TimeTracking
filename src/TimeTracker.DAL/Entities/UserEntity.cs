namespace TimeTracker.DAL.Entities;

/// <summary>
/// Represents an employee
/// </summary>
public class UserEntity
{
    public int Id { get; set; }

    public WorkScheduleEntity? WorkSchedule { get; set; }
    public ICollection<CardEntity> Cards { get; set; } = [];
    public ICollection<AttendanceRecordEntity> AttendanceRecords { get; set; } = [];
    public ICollection<ScheduleExclusionEntity> ScheduleExclusions { get; set; } = [];
}
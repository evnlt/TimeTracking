namespace TimeTracker.DAL.Entities;

/// <summary>
/// Represents an employee
/// </summary>
// TODO - rename to employee
public class UserEntity
{
    public int Id { get; set; }

    public WorkScheduleEntity? WorkSchedule { get; set; }
    public ICollection<CardEntity> Cards { get; set; } = [];
    public ICollection<AttendanceRecordEntity> AttendanceRecords { get; set; } = [];
    public ICollection<ScheduleExclusionEntity> ScheduleExclusions { get; set; } = [];
}
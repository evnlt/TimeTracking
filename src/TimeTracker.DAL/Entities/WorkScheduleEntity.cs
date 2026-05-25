namespace TimeTracker.DAL.Entities;

/// <summary>
/// Represents an employee work schedule configuration.
/// </summary>
public class WorkScheduleEntity
{
    public int UserId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool FreeSchedule { get; set; }
    public List<DayOfWeek> WorkingDays { get; set; } = [];
    
    public UserEntity User { get; set; } = default!;
}
using Constants.Enums;

namespace TimeTracker.DAL.Entities;

/// <summary>
/// Represents a temporary exception to the employee work schedule.
/// </summary>
public class ScheduleExclusionEntity
{
    public int Id { get; set; }
    public ExclusionType Type { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    
    public int UserId { get; set; }
    
    public UserEntity User { get; set; } = default!;
}
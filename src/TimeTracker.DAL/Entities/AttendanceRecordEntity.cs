namespace TimeTracker.DAL.Entities;

/// <summary>
/// Represents employee attendance for a specific work day.
/// </summary>
public class AttendanceRecordEntity
{
    public int Id { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    
    public int UserId { get; set; }
    
    public UserEntity User { get; set; } = default!;
}
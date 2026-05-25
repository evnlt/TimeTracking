using Constants.Enums;

namespace TimeTracker.DAL.Entities;

public class AttendanceHistoryEntity
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public AttendanceAction Action { get; set; }
    
    public int UserId { get; set; }
    
    public UserEntity User { get; set; } = default!;
}

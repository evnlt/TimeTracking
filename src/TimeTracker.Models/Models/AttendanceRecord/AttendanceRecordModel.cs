namespace TimeTracker.Models.Models.AttendanceRecord;

public class AttendanceRecordModel
{
    public int Id { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    
    public int UserId { get; set; }
}
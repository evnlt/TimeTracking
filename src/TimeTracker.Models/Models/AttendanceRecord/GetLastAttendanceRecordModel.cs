namespace TimeTracker.Models.Models.AttendanceRecord;

public class GetLastAttendanceRecordModel
{
    public int UserId { get; set; }
    public DateOnly AttendanceDate { get; set; }
}
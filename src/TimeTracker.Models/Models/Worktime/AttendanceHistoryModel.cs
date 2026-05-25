using Constants.Enums;

namespace TimeTracker.Models.Models.WorkTime;

public class AttendanceHistoryModel
{
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public AttendanceAction Action { get; set; }
}
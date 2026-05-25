using Constants.Enums;

namespace TimeTracker.API.Models.Worktime;

public class AttendanceHistoryResponse
{
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public AttendanceAction Action { get; set; }
}
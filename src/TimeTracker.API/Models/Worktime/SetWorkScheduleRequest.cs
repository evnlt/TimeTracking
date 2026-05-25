namespace TimeTracker.API.Models.Worktime;

public class SetWorkScheduleRequest
{
    public int UserId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<DayOfWeek> Days { get; set; } = [];
    public bool FreeSchedule { get; set; }
}
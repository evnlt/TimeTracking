namespace TimeTracker.Models.Models.WorkTime;

public class WorkScheduleModel
{
    public int UserId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public List<DayOfWeek> Days { get; set; } = [];
    public bool FreeSchedule { get; set; }
}
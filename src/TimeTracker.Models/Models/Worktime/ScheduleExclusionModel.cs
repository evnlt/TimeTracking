using Constants.Enums;

namespace TimeTracker.Models.Models.WorkTime;

public class ScheduleExclusionModel
{
    public int UserId { get; set; }
    public ExclusionType Type { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
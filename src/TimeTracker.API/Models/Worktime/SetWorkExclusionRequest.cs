using Constants.Enums;

namespace TimeTracker.API.Models.Worktime;

public class SetWorkExclusionRequest
{
    public int UserId { get; set; }
    public ExclusionType Type { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
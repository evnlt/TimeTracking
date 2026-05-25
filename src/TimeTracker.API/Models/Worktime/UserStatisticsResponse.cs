namespace TimeTracker.API.Models.Worktime;

public class UserStatisticsResponse
{
    public int UserId { get; set; }

    public TimeSpan ExpectedWork { get; set; }
    public TimeSpan Worked { get; set; }
    public TimeSpan Missing { get; set; }

    public int LateCount { get; set; }
    public int EarlyLeaveCount { get; set; }

    public int LateWithReason { get; set; }
    public int LateWithoutReason { get; set; }

    public int EarlyWithReason { get; set; }
    public int EarlyWithoutReason { get; set; }
}
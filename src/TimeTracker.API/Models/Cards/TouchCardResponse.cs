using Constants.Enums;

namespace TimeTracker.API.Models.Cards;

public class TouchCardResponse
{
    public string CardUid { get; set; } = default!;
    public int UserId { get; set; }
    public AttendanceAction Action { get; set; }
}
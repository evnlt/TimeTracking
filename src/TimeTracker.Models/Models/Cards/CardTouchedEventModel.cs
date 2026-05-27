using Constants.Enums;

namespace TimeTracker.Models.Models.Cards;

public class CardTouchedEventModel
{
    public string CardUid { get; set; } = default!;
    public int UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public AttendanceAction Action { get; set; }
}
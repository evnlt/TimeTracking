namespace TimeTracker.Models.Models.Cards;

public class CardModel
{
    public string CardUid { get; set; } = default!;
    public int UserId { get; set; }
    public DateTime AssignedAt { get; set; }
}
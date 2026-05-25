namespace TimeTracker.Models.Models.Cards;

public class AssignUserModel
{
    public string CardUid { get; set; } = default!;
    public int UserId { get; set; }
}
namespace TimeTracker.API.Models.Cards;

public class AssignUserResponse
{
    public int UserId { get; set; }
    public string CardUId { get; set; } = default!;
}
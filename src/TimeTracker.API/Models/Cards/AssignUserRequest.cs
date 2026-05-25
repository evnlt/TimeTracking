namespace TimeTracker.API.Models.Cards;

public class AssignUserRequest
{
    public int UserId { get; set; }
    public string CardUId { get; set; } = default!;
}
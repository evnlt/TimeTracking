namespace TimeTracker.API.Models.Cards;

public class DeleteAllUserCardsResponse
{
    public int UserId { get; set; }
    public string[] DeletedCards { get; set; } = [];
}
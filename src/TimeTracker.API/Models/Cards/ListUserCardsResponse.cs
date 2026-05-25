namespace TimeTracker.API.Models.Cards;

public class ListUserCardsResponse
{
    public int UserId { get; set; }
    public string[] Cards { get; set; } = [];
}
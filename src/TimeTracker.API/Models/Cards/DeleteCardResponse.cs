namespace TimeTracker.API.Models.Cards;

public class DeleteCardResponse
{
    public int UserId { get; set; }
    public string CardUId { get; set; } = default!;
}
using TimeTracker.DAL.Entities;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.DAL.Extensions.EntityExtensions;

public static class CardEntityExtensions
{
    public static CardModel ToModel(this CardEntity entity)
    {
        return new CardModel
        {
            CardUid = entity.CardUid,
            UserId = entity.UserId,
            AssignedAt = entity.AssignedAt
        };
    }
}
using TimeTracker.API.Models.Cards;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.API.Extensions.Models;

public static class CardExtensions
{
    public static TouchCardModel ToModel(this TouchCardRequest request)
    {
        return new TouchCardModel
        {
            CardUid = request.CardUid
        };
    }

    public static AssignUserModel ToModel(this AssignUserRequest request)
    {
        return new AssignUserModel
        {
            UserId = request.UserId,
            CardUid = request.CardUId
        };
    }

    public static ListByUserModel ToModel(this ListUserRequest request)
    {
        return new ListByUserModel
        {
            UserId = request.UserId
        };
    }

    public static DeleteCardModel ToModel(this DeleteCardRequest request)
    {
        return new DeleteCardModel
        {
            CardUid = request.CardUId
        };
    }

    public static DeleteAllCardsByUserModel ToModel(this DeleteAllCardsByUserRequest request)
    {
        return new DeleteAllCardsByUserModel
        {
            UserId = request.UserId
        };
    }
    
    public static ListUserCardsResponse ToResponse(this CardModel[] cards, int userId)
    {
        return new ListUserCardsResponse
        {
            UserId = userId,
            Cards = cards.Select(x => x.CardUid).ToArray()// TODO - refactor??
        };
    }
}
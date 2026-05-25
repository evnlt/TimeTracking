using TimeTracker.Models.Models.Cards;

namespace TimeTracker.BLL.Abstraction;

public interface ICardService
{
    Task<Result> Touch(TouchCardModel model);
    Task<Result> AssignUser(AssignUserModel model);
    Task<Result<CardModel[]>> ListByUser(ListByUserModel model);
    Task<Result> Delete(DeleteCardModel model);
    Task<Result> DeleteAllByUser(DeleteAllCardsByUserModel model);
}
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.DAL.Abstraction;

public interface ICardStore
{
    Task<CardModel?> GetByUid(string id);
    Task<CardModel[]> GetByUserId(int userId);
    Task Assign(AssignUserModel model, DateTime assignedAt);
    Task Delete(string id);
    Task DeleteAllByUserId(int userId);
    
    Task<bool> DoesExist(string id);
}
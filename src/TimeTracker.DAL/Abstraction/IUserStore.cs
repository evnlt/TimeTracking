namespace TimeTracker.DAL.Abstraction;

public interface IUserStore
{
    Task<bool> DoesExist(int id);
}
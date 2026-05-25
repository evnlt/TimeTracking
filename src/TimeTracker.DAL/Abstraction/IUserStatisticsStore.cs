using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Abstraction;

public interface IUserStatisticsStore
{
    Task<UserStatisticsModel?> GetByUser(int userId);
    Task<UserStatisticsModel[]> GetAll(int limit);
    // TODO - create UpsertUserStatisticsModel
    Task Upsert(UserStatisticsModel model);
}
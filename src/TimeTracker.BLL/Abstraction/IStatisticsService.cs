using Constants;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Abstraction;

public interface IStatisticsService
{
    Task<Result<UserStatisticsModel>> GetByUser(int userId);

    Task<Result<UserStatisticsModel[]>> GetMany(OffsetPagination pager); 
}
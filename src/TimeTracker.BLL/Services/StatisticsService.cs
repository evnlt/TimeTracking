using Constants;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Utilities;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Services;

public class StatisticsService : IStatisticsService
{
    private readonly StatisticsValidator _statisticsValidator;
    private readonly IUserStatisticsStore _userStatisticsStore;

    public StatisticsService(StatisticsValidator statisticsValidator, IUserStatisticsStore userStatisticsStore)
    {
        _statisticsValidator = statisticsValidator;
        _userStatisticsStore = userStatisticsStore;
    }

    public async Task<Result<UserStatisticsModel>> GetByUser(int userId)
    {
        var validationResult = await _statisticsValidator.Validate(userId);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<UserStatisticsModel>();
        }
        
        var statistics = await _userStatisticsStore.GetByUser(userId);
        
        if (statistics == null)
        {
            return Result<UserStatisticsModel>.Fail("Statistic not found", ErrorType.NotFound);
        }
        
        var result = Result<UserStatisticsModel>.Ok(statistics);

        return result;
    }

    public async Task<Result<UserStatisticsModel[]>> GetMany(OffsetPagination pager)
    {
        var validationResult = _statisticsValidator.Validate(pager);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<UserStatisticsModel[]>();
        }
        
        var statistics = await  _userStatisticsStore.GetAll(pager);
        
        var result = Result<UserStatisticsModel[]>.Ok(statistics);

        return result;
    }
}
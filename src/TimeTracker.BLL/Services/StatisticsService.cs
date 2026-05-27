using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Utilities;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Services;

public class StatisticsService : IStatisticsService
{
    // TODO - refactor
    private readonly StatisticsValidator _statisticsValidator;
    private readonly IUserStatisticsStore _userStatisticsStore;

    public StatisticsService(StatisticsValidator statisticsValidator, IUserStatisticsStore userStatisticsStore)
    {
        _statisticsValidator = statisticsValidator;
        _userStatisticsStore = userStatisticsStore;
    }

    public async Task<Result<UserStatisticsModel>> GetByUser(int userId)
    {
        var validationResult = await _statisticsValidator.ValidateUser(userId);
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

    public async Task<Result<UserStatisticsModel[]>> GetAll(int limit) // TODO - pagination?
    {
        var validationResult = await _statisticsValidator.ValidateLimit(limit);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<UserStatisticsModel[]>();
        }
        
        var statistics = await  _userStatisticsStore.GetAll(limit);
        
        var result = Result<UserStatisticsModel[]>.Ok(statistics);

        return result;
    }
}
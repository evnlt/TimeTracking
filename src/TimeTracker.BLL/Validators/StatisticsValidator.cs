using TimeTracker.DAL.Abstraction;

namespace TimeTracker.BLL.Validators;

public class StatisticsValidator
{
    private readonly IUserStore _userStore;

    public StatisticsValidator(IUserStore userStore)
    {
        _userStore = userStore;
    }
    
    // TODO refactor
    public async Task<Result> ValidateUser(int userId)
    {
        var userExists = await _userStore.DoesExist(userId);
        if (!userExists)
        {
            return Result.Fail("User does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    // TODO refactor
    public async Task<Result> ValidateLimit(int limit)
    {
        // TODO validation

        return Result.Ok();
    }
}
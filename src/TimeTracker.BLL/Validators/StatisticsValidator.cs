using Constants;
using TimeTracker.BLL.Exceptions;
using TimeTracker.DAL.Abstraction;

namespace TimeTracker.BLL.Validators;

public class StatisticsValidator
{
    private readonly IUserStore _userStore;

    public StatisticsValidator(IUserStore userStore)
    {
        _userStore = userStore;
    }
    
    public async Task<Result> Validate(int userId)
    {
        var userExists = await _userStore.DoesExist(userId);
        if (!userExists)
        {
            return Result.Fail(ErrorMessages.UserNotFound, ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public Result Validate(OffsetPagination pager)
    {
        if (pager.Offset < 0)
        {
            return Result.Fail(ErrorMessages.NegativeOffset, ErrorType.Validation);
        }

        if (pager.Take <= 0)
        {
            return Result.Fail(ErrorMessages.TakeLessThatOne, ErrorType.Validation);
        }

        return Result.Ok();
    }
}
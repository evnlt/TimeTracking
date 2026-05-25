using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Validators;

public class WorkTimeValidator
{
    private readonly IUserStore _userStore;

    public WorkTimeValidator(IUserStore userStore)
    {
        _userStore = userStore;
    }
    
    public async Task<Result> Validate(int userId)
    {
        var userExists = await _userStore.DoesExist(userId);
        if (!userExists)
        {
            return Result.Fail("User does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(WorkScheduleModel model)
    {
        if (model == null)
        {
            return Result.Fail("Model is null", ErrorType.Validation);
        }
        
        // TODO - do more validation

        var userExists = await _userStore.DoesExist(model.UserId);
        if (!userExists)
        {
            return Result.Fail("User does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
    
    public async Task<Result> Validate(ScheduleExclusionModel model)
    {
        if (model == null)
        {
            return Result.Fail("Model is null", ErrorType.Validation);
        }
        
        // TODO - do more validation

        var userExists = await _userStore.DoesExist(model.UserId);
        if (!userExists)
        {
            return Result.Fail("User does not exist", ErrorType.NotFound);
        }

        return Result.Ok();
    }
}
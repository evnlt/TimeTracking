using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Utilities;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Services;

public class WorkTimeService : IWorkTimeService
{
    private readonly WorkTimeValidator _workTimeValidator;
    private readonly IWorkScheduleStore _workScheduleStore;
    private readonly IScheduleExclusionStore _scheduleExclusionStore;

    public WorkTimeService(
        WorkTimeValidator workTimeValidator,
        IWorkScheduleStore workScheduleStore,
        IScheduleExclusionStore scheduleExclusionStore)
    {
        _workTimeValidator = workTimeValidator;
        _workScheduleStore = workScheduleStore;
        _scheduleExclusionStore = scheduleExclusionStore;
    }

    public async Task<Result> Set(WorkScheduleModel model)
    {
        // TODO - maybe refactor to make this shorter
        var validationResult = await _workTimeValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }
        
        await _workScheduleStore.Set(model);
        
        return Result.Ok();
    }

    public async Task<Result<WorkScheduleModel>> Get(int userId)
    {
        var validationResult = await _workTimeValidator.Validate(userId);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<WorkScheduleModel>();
        }
        
        WorkScheduleModel? workSchedule = await _workScheduleStore.Get(userId);

        if (workSchedule == null)
        {
            return Result<WorkScheduleModel>.Fail("Word schedule not found", ErrorType.NotFound);
        }
        
        var result = Result<WorkScheduleModel>.Ok(workSchedule);

        return result;
    }

    public async Task<Result> AddExclusion(ScheduleExclusionModel model)
    {
        var validationResult = await _workTimeValidator.Validate(model);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }
        
        await _scheduleExclusionStore.Add(model);
        return Result.Ok();
    }

    public async Task<Result<ScheduleExclusionModel[]>> GetExclusions(int userId)
    {
        var validationResult = await _workTimeValidator.Validate(userId);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<ScheduleExclusionModel[]>();
        }
        
        var workExclusions = await _scheduleExclusionStore.GetByUser(userId);
        
        var result = Result<ScheduleExclusionModel[]>.Ok(workExclusions);

        return result;
    }
}
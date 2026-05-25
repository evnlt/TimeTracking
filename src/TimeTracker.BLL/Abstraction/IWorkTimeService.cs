using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Abstraction;

public interface IWorkTimeService
{
    Task<Result> Set(WorkScheduleModel model);
    // TODO - create a model for this method
    Task<Result<WorkScheduleModel>> Get(int userId);
    Task<Result> AddExclusion(ScheduleExclusionModel model);
    Task<Result<ScheduleExclusionModel[]>> GetExclusions(int userId);
}
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Abstraction;

public interface IScheduleExclusionStore
{
    // TODO - create an independent AddScheduleExclusionModel in the future
    Task Add(ScheduleExclusionModel model);
    Task<ScheduleExclusionModel[]> GetByUser(int userId);
}
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Abstraction;

public interface IWorkScheduleStore
{
    // TODO - create an independent SetWorkScheduleModel in the future
    Task Set(WorkScheduleModel model);
    Task<WorkScheduleModel?> Get(int userId);
}
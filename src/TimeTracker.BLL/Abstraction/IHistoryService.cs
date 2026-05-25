using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Abstraction;

public interface IHistoryService
{
    Task<Result<AttendanceHistoryModel[]>> GetByUser(int userId);
    Task<Result<AttendanceHistoryModel[]>> GetAll(int limit);
}
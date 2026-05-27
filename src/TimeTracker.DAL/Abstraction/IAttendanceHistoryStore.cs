using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Abstraction;

public interface IAttendanceHistoryStore
{
    // TODO - create separate CreateAttendanceHistoryModel
    Task Create(AttendanceHistoryModel model);
    Task<AttendanceHistoryModel[]> GetByUser(int userId);
    Task<AttendanceHistoryModel[]> GetAll(int limit);
}
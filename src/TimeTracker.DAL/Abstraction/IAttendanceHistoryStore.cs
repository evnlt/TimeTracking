using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Abstraction;

public interface IAttendanceHistoryStore
{
    Task<AttendanceHistoryModel[]> GetByUser(int userId);
    Task<AttendanceHistoryModel[]> GetAll(int limit);
}
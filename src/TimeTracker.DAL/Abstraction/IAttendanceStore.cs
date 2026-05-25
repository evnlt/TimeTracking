using TimeTracker.Models.Models.AttendanceRecord;

namespace TimeTracker.DAL.Abstraction;

public interface IAttendanceStore
{
    Task<AttendanceRecordModel?> GetLastByUser(GetLastAttendanceRecordModel model);
    Task Create(CreateAttendanceRecordModel model);
    Task Update(AttendanceRecordModel model);
}
using Constants;
using TimeTracker.BLL.Abstraction;
using TimeTracker.BLL.Utilities;
using TimeTracker.BLL.Validators;
using TimeTracker.DAL.Abstraction;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.BLL.Services;

public class HistoryService : IHistoryService
{
    private readonly HistoryValidator _historyValidator;
    private readonly IAttendanceHistoryStore _attendanceHistoryStore;

    public HistoryService(HistoryValidator historyValidator,
        IAttendanceHistoryStore attendanceHistoryStore)
    {
        _historyValidator = historyValidator;
        _attendanceHistoryStore = attendanceHistoryStore;
    }

    public async Task<Result<AttendanceHistoryModel[]>> GetByUser(int userId)
    {
        var validationResult = await _historyValidator.ValidateUser(userId);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<AttendanceHistoryModel[]>();
        }

        var attendanceHistories = await _attendanceHistoryStore.GetByUser(userId);
        return Result<AttendanceHistoryModel[]>.Ok(attendanceHistories);
    }

    public async Task<Result<AttendanceHistoryModel[]>> GetMany(OffsetPagination pager)
    {
        var validationResult = _historyValidator.Validate(pager);
        if (!validationResult.IsSuccess)
        {
            return validationResult.As<AttendanceHistoryModel[]>();
        }

        var attendanceHistories = await _attendanceHistoryStore.GetMany(pager);
        return Result<AttendanceHistoryModel[]>.Ok(attendanceHistories);
    }
}
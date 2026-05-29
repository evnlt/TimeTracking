using TimeTracker.API.Models.Worktime;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.API.Extensions.Models;

public static class WorkTimeExtensions
{
    public static WorkScheduleModel ToModel(this SetWorkScheduleRequest request)
    {
        return new WorkScheduleModel
        {
            UserId = request.UserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Days = request.Days,
            FreeSchedule = request.FreeSchedule
        };
    }
    
    public static ScheduleExclusionModel ToModel(this SetWorkExclusionRequest request)
    {
        return new ScheduleExclusionModel
        {
            UserId = request.UserId,
            Type = request.Type,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime
        };
    }
    
    public static WorkExclusionResponse ToResponse(this ScheduleExclusionModel model)
    {
        return new WorkExclusionResponse
        {
            UserId = model.UserId,
            Type = model.Type,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime
        };
    }
    
    public static AttendanceHistoryResponse ToResponse(this AttendanceHistoryModel model)
    {
        return new AttendanceHistoryResponse
        {
            UserId = model.UserId,
            Timestamp = model.Timestamp,
            Action = model.Action
        };
    }
    
    public static UserStatisticsResponse ToResponse(this UserStatisticsModel model)
    {
        return new UserStatisticsResponse
        {
            UserId = model.UserId,
            ExpectedWork = model.ExpectedWork,
            Worked = model.Worked,
            Missing = model.Missing,
            LateCount = model.LateCount,
            EarlyLeaveCount = model.EarlyLeaveCount,
            LateWithReason = model.LateWithReason,
            LateWithoutReason = model.LateWithoutReason,
            EarlyWithReason = model.EarlyWithReason,
            EarlyWithoutReason = model.EarlyWithoutReason
        };
    }
    
    public static WorkScheduleResponse ToResponse(this WorkScheduleModel model)
    {
        return new WorkScheduleResponse
        {
            UserId = model.UserId,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            Days = model.Days,
            FreeSchedule = model.FreeSchedule
        };
    }
}
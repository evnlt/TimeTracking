using Constants.Enums;
using TimeTracker.DAL.Entities;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Extensions.EntityExtensions;

public static class AttendanceHistoryExtensions
{
    public static AttendanceHistoryModel[] ToModels(this AttendanceRecordEntity entity)
    {
        var result = new List<AttendanceHistoryModel>
        {
            new()
            {
                UserId = entity.UserId,
                Timestamp = entity.CheckIn,
                Action = AttendanceAction.CheckIn
            }
        };

        if (entity.CheckOut.HasValue)
        {
            result.Add(new AttendanceHistoryModel
            {
                UserId = entity.UserId,
                Timestamp = entity.CheckOut.Value,
                Action = AttendanceAction.CheckOut
            });
        }

        return result.ToArray();
    }
}
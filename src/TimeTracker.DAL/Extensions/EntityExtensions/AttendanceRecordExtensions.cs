using TimeTracker.DAL.Entities;
using TimeTracker.Models.Models.AttendanceRecord;

namespace TimeTracker.DAL.Extensions.EntityExtensions;

public static class AttendanceRecordExtensions
{
    public static AttendanceRecordModel ToModel(
        this AttendanceRecordEntity entity)
    {
        return new AttendanceRecordModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            AttendanceDate = entity.AttendanceDate,
            CheckIn = entity.CheckIn,
            CheckOut = entity.CheckOut
        };
    }
    
    public static AttendanceRecordEntity ToEntity(
        this CreateAttendanceRecordModel model)
    {
        return new AttendanceRecordEntity
        {
            UserId = model.UserId,
            AttendanceDate = model.AttendanceDate,
            CheckIn = model.CheckIn,
            CheckOut = model.CheckOut
        };
    }
}
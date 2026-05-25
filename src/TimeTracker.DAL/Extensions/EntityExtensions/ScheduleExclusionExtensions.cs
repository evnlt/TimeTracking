using TimeTracker.DAL.Entities;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Extensions.EntityExtensions;

public static class ScheduleExclusionExtensions
{
    public static ScheduleExclusionModel ToModel(this ScheduleExclusionEntity entity)
    {
        return new ScheduleExclusionModel
        {
            UserId = entity.UserId,
            Type = entity.Type,
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime
        };
    }
}
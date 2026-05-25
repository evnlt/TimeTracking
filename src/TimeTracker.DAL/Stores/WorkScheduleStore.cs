using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Entities;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Stores;

public class WorkScheduleStore : IWorkScheduleStore
{
    private readonly AppDbContext _appDbContext;

    public WorkScheduleStore(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Set(WorkScheduleModel model)
    {
        var entity = await _appDbContext.WorkSchedules
            .FirstOrDefaultAsync(x => x.UserId == model.UserId);

        if (entity == null)
        {
            entity = new WorkScheduleEntity
            {
                UserId = model.UserId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                WorkingDays = model.Days,
                FreeSchedule = model.FreeSchedule
            };

            await _appDbContext.WorkSchedules.AddAsync(entity);
        }
        else
        {
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.WorkingDays = model.Days;
            entity.FreeSchedule = model.FreeSchedule;

            _appDbContext.WorkSchedules.Update(entity);
        }

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<WorkScheduleModel?> Get(int userId)
    {
        var entity = await _appDbContext.WorkSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (entity == null)
        {
            return null;
        }

        return new WorkScheduleModel
        {
            UserId = entity.UserId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Days = entity.WorkingDays,
            FreeSchedule = entity.FreeSchedule
        };
    }
}
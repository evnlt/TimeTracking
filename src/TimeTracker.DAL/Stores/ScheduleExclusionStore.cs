using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Extensions.EntityExtensions;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Stores;

public class ScheduleExclusionStore : IScheduleExclusionStore
{
    private readonly AppDbContext _dbContext;

    public ScheduleExclusionStore(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ScheduleExclusionModel model)
    {
        var entity = new ScheduleExclusionEntity
        {
            UserId = model.UserId,
            Type = model.Type,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime
        };

        await _dbContext.ScheduleExclusions.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ScheduleExclusionModel[]> GetByUser(int userId)
    {
        var entities = await _dbContext.ScheduleExclusions
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.StartDateTime)
            .ToArrayAsync();

        return entities
            .Select(_ => _.ToModel())
            .ToArray();
    }
}
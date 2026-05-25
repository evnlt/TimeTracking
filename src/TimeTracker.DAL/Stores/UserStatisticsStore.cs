using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Entities;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Stores;

public class UserStatisticsStore : IUserStatisticsStore
{
    private readonly AppDbContext _appDbContext;

    public UserStatisticsStore(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<UserStatisticsModel?> GetByUser(int userId)
    {
        // TODO - figure out tracking
        var entity = await _appDbContext.UserStatistics
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return entity == null ? null : ToModel(entity);
    }

    public async Task<UserStatisticsModel[]> GetAll(int limit)
    {
        var entities = await _appDbContext.UserStatistics
            .AsNoTracking()
            .OrderByDescending(x => x.UserId)
            .Take(limit)
            .ToArrayAsync();

        return entities
            .Select(ToModel)
            .ToArray();
    }

    public async Task Upsert(UserStatisticsModel model)
    {
        var entity = await _appDbContext.UserStatistics
            .FirstOrDefaultAsync(x => x.UserId == model.UserId);

        if (entity == null)
        {
            entity = new UserStatisticsEntity
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

            await _appDbContext.UserStatistics.AddAsync(entity);
        }
        else
        {
            entity.ExpectedWork = model.ExpectedWork;
            entity.Worked = model.Worked;
            entity.Missing = model.Missing;
            entity.LateCount = model.LateCount;
            entity.EarlyLeaveCount = model.EarlyLeaveCount;
            entity.LateWithReason = model.LateWithReason;
            entity.LateWithoutReason = model.LateWithoutReason;
            entity.EarlyWithReason = model.EarlyWithReason;
            entity.EarlyWithoutReason = model.EarlyWithoutReason;

            _appDbContext.UserStatistics.Update(entity);
        }

        await _appDbContext.SaveChangesAsync();
    }

    private static UserStatisticsModel ToModel(UserStatisticsEntity entity)
    {
        return new UserStatisticsModel
        {
            UserId = entity.UserId,
            ExpectedWork = entity.ExpectedWork,
            Worked = entity.Worked,
            Missing = entity.Missing,
            LateCount = entity.LateCount,
            EarlyLeaveCount = entity.EarlyLeaveCount,
            LateWithReason = entity.LateWithReason,
            LateWithoutReason = entity.LateWithoutReason,
            EarlyWithReason = entity.EarlyWithReason,
            EarlyWithoutReason = entity.EarlyWithoutReason
        };
    }
}
using Constants;
using Constants.Enums;
using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Extensions.EntityExtensions;
using TimeTracker.Models.Models.WorkTime;

namespace TimeTracker.DAL.Stores;

public class AttendanceHistoryStore : IAttendanceHistoryStore
{
    private readonly AppDbContext _appDbContext;

    public AttendanceHistoryStore(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task Create(AttendanceHistoryModel model)
    {
        var entity = new AttendanceRecordEntity
        {
            UserId = model.UserId,
            AttendanceDate = DateOnly.FromDateTime(model.Timestamp),

            CheckIn = model.Action == AttendanceAction.CheckIn
                ? model.Timestamp
                : default,

            CheckOut = model.Action == AttendanceAction.CheckOut
                ? model.Timestamp
                : null
        };

        await _appDbContext.AttendanceRecords.AddAsync(entity);

        await _appDbContext.SaveChangesAsync();
    }
    
    public async Task<AttendanceHistoryModel[]> GetByUser(int userId)
    {
        var entities = await _appDbContext.AttendanceRecords
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CheckIn)
            .ToArrayAsync();

        return entities
            .SelectMany(_ => _.ToModels())
            .ToArray();
    }

    public async Task<AttendanceHistoryModel[]> GetMany(OffsetPagination pager)
    {
        var entities = await _appDbContext.AttendanceRecords
            .AsNoTracking()
            .OrderByDescending(x => x.CheckIn)
            .Skip(pager.Offset)
            .Take(pager.Take)
            .ToArrayAsync();

        return entities
            .SelectMany(_ => _.ToModels())
            .ToArray();
    }
}
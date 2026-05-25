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

    public async Task<AttendanceHistoryModel[]> GetAll(int limit)
    {
        var entities = await _appDbContext.AttendanceRecords
            .AsNoTracking()
            .OrderByDescending(x => x.CheckIn)
            .Take(limit)
            .ToArrayAsync();

        return entities
            .SelectMany(_ => _.ToModels())
            .ToArray();
    }
}
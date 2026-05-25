using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Extensions.EntityExtensions;
using TimeTracker.Models.Models.AttendanceRecord;

namespace TimeTracker.DAL.Stores;

public class AttendanceStore : IAttendanceStore
{
    private readonly AppDbContext _appDbContext;

    public AttendanceStore(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<AttendanceRecordModel?> GetLastByUser(GetLastAttendanceRecordModel model)
    {
        var entity = await _appDbContext.AttendanceRecords
            .Where(_ =>
                _.UserId == model.UserId &&
                _.AttendanceDate == model.AttendanceDate)
            .OrderByDescending(x => x.CheckIn)
            .FirstOrDefaultAsync();

        return entity?.ToModel();
    }

    public async Task Create(CreateAttendanceRecordModel model)
    {
        var entity = model.ToEntity();

        await _appDbContext.AttendanceRecords.AddAsync(entity);

        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(AttendanceRecordModel model)
    {
        await _appDbContext.AttendanceRecords
            .Where(_ => _.Id == model.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(_ => _.CheckIn, model.CheckIn)
                .SetProperty(_ => _.CheckOut, model.CheckOut));
    }
}
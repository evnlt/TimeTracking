using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;
using TimeTracker.DAL.Extensions.EntityExtensions;
using TimeTracker.Models.Models.Cards;

namespace TimeTracker.DAL.Stores;

public class CardStore : ICardStore
{
    private readonly AppDbContext _appDbContext;

    public CardStore(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<CardModel?> GetByUid(string id)
    {
        var entity = await _appDbContext.Cards
            .FirstOrDefaultAsync(_ => _.CardUid == id);

        return entity?.ToModel();
    }

    public async Task<CardModel[]> GetByUserId(int userId)
    {
        return await _appDbContext.Cards
            .Where(_ => _.UserId == userId)
            .Select(_ => _.ToModel())
            .ToArrayAsync();
    }

    public async Task Assign(AssignUserModel model, DateTime assignedAt)
    {
        // TODO - refactor to execute update
        var entity = await _appDbContext.Cards
            .FirstAsync(_ => _.CardUid == model.CardUid);

        entity.UserId = model.UserId;
        entity.AssignedAt = assignedAt;

        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        var entity = await _appDbContext.Cards
            .FirstAsync(_ => _.CardUid == id);

        _appDbContext.Cards.Remove(entity);

        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAllByUserId(int userId)
    {
        await _appDbContext.Cards
            .Where(_ => _.UserId == userId)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> DoesExist(string id)
    {
        return await _appDbContext.Cards.AnyAsync(_ => _.CardUid == id);
    }
}
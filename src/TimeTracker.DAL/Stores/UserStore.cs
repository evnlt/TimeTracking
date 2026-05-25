using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Abstraction;

namespace TimeTracker.DAL.Stores;

public class UserStore : IUserStore
{
    private readonly AppDbContext _appDbContext;

    public UserStore(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<bool> DoesExist(int id)
    {
        return await _appDbContext.Users.AnyAsync(_ => _.Id == id);
    }
}
using Domain.Entity;
using Domain.Repository;
using Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository;

public class UserRepository : IUserRepository
{
    private DbSet<User> _users;

    public UserRepository(WarningDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<User> GetAsync(Guid id)
        => await _users.FirstOrDefaultAsync(x => x.Id == id);
}

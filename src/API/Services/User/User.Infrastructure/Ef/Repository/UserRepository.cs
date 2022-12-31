using Microsoft.EntityFrameworkCore;
using User.Domain.Repository;
using User.Domain.ValueObject;
using User.Infrastructure.Ef.Context;

namespace User.Infrastructure.Ef.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;
    private readonly DbSet<Domain.Entity.User> _users;
    public UserRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = dbContext.Users;
    }

    public async Task AddAsync(Domain.Entity.User user)
    {
        await _users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Domain.Entity.User user)
    {
        _users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entity.User> GetAsync(UserId id)
    {
        return await _users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task UpdateAsync(Domain.Entity.User user)
    {
        _users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}

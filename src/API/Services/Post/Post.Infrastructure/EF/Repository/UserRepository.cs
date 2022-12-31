using Microsoft.EntityFrameworkCore;
using Post.Domain.Entity;
using Post.Domain.Repository;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.Repository;

public class UserRepository : IUserRepository
{
    private DbSet<User> _users;

    public UserRepository(PostDbContext dbContext)
    {
        _users = dbContext.Users;
    }

    public async Task<User> GetAsync(Guid id)
        => await _users.Include("_posts").Include("_comments").FirstOrDefaultAsync(x => x.Id == id);
}

using Microsoft.EntityFrameworkCore;
using User.Domain.Entity;
using User.Infrastructure.Ef.Config.WritecConfig;

namespace User.Infrastructure.Ef.Context;

public class UserDbContext : DbContext
{
    public DbSet<Domain.Entity.User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfig().Configure(modelBuilder.Entity<Domain.Entity.User>());
        new UserFriendConfig().Configure(modelBuilder.Entity<UserFriend>());
    }
}

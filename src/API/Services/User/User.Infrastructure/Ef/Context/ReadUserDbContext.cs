using Microsoft.EntityFrameworkCore;
using User.Application.ReadModel;
using User.Infrastructure.Ef.Config;

namespace User.Infrastructure.Ef.Context;

public class ReadUserDbContext : DbContext
{
    public DbSet<UserReadModel> Users { get; set; }

    public ReadUserDbContext(DbContextOptions<ReadUserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ReadUserConfig().Configure(modelBuilder.Entity<UserReadModel>());
    }
}

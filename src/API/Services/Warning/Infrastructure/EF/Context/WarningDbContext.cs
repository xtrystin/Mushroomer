using Domain;
using Domain.Entity;
using Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Context;

public sealed class WarningDbContext : DbContext
{
    public DbSet<Warning> Warnings { get; set; }
    public DbSet<User> Users { get; set; }

	public WarningDbContext(DbContextOptions<WarningDbContext> options) : base(options)
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new WarningConfig().Configure(modelBuilder.Entity<Warning>());
        new WarningUserReactionConfig().Configure(modelBuilder.Entity<WarningUserReaction>());
        new UserConfig().Configure(modelBuilder.Entity<User>());
    }
}

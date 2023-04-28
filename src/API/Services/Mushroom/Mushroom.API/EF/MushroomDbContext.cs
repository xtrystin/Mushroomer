using Microsoft.EntityFrameworkCore;
using Mushroom.API.Model;

namespace WebApplication1.EF;

public class MushroomDbContext : DbContext
{
    public DbSet<Mushroom.API.Model.Mushroom> Mushrooms { get; set; }

    public MushroomDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new MushroomConfig().Configure(modelBuilder.Entity<Mushroom.API.Model.Mushroom>());
    }
}

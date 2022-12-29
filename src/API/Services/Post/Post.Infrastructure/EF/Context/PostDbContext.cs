using Microsoft.EntityFrameworkCore;
using Post.Infrastructure.EF.Config;

namespace Post.Infrastructure.EF.Context;

public sealed class PostDbContext : DbContext
{
    public DbSet<Domain.Entity.Post> Posts { get; set; }

    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PostConfig().Configure(modelBuilder.Entity<Domain.Entity.Post>());
        new CommentConfig().Configure(modelBuilder.Entity<Domain.Entity.Comment>());
    }
}

using Microsoft.EntityFrameworkCore;
using Post.Infrastructure.EF.Config;
using Post.Infrastructure.EF.Config.WritingConfig;

namespace Post.Infrastructure.EF.Context;

public sealed class PostDbContext : DbContext
{
    public DbSet<Domain.Entity.Post> Posts { get; set; }
    public DbSet<Domain.Entity.User> Users { get; set; }

    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new PostConfig().Configure(modelBuilder.Entity<Domain.Entity.Post>());
        new CommentConfig().Configure(modelBuilder.Entity<Domain.Entity.Comment>());
        new UserConfig().Configure(modelBuilder.Entity<Domain.Entity.User>());
    }
}

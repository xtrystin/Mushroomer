using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.ReadModel;
using Post.Infrastructure.EF.Config.ReadingConfig;

namespace Post.Infrastructure.EF.Context;

public sealed class ReadPostDbContext : DbContext
{
    public DbSet<PostReadModel> Posts { get; set; }

    public ReadPostDbContext(DbContextOptions<ReadPostDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ReadPostConfig().Configure(modelBuilder.Entity<PostReadModel>());
        new ReadCommentConfig().Configure(modelBuilder.Entity<CommentReadModel>());
        new ReadUserConfig().Configure(modelBuilder.Entity<UserReadModel>());
        new ReadPostUserReactionConfig().Configure(modelBuilder.Entity<PostUserReactionReadModel>());
    }
}

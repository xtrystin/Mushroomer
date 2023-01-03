using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entity;
using Post.Application.ReadModel;

namespace Post.Infrastructure.EF.Config.ReadingConfig;

public class ReadPostUserReactionConfig : IEntityTypeConfiguration<PostUserReactionReadModel>
{
    public void Configure(EntityTypeBuilder<PostUserReactionReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Like)
            .IsRequired()
            .HasColumnName("Like");

        builder.Property(x => x.UserId)
            .HasColumnName("UserId");

        builder.Property(x => x.PostId)
            .HasColumnName("PostId");

        builder.HasOne(x => x.Post)
            .WithMany(x => x.Reactions)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.PostId);

        builder.HasOne(x => x.User)
            .WithMany("_postReactions")
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.UserId);

        builder.ToTable("PostUserReaction");
    }
}

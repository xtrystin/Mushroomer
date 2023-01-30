using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entity;
using Post.Domain.ValueObject;

namespace Post.Infrastructure.EF.Config.WritingConfig;

public class PostUserReactionConfig : IEntityTypeConfiguration<PostUserReaction>
{
    public void Configure(EntityTypeBuilder<PostUserReaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Like)
            .IsRequired()
            .HasColumnName("Like")
            .HasColumnType("boolean");

        builder.Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("uuid");
        
        builder.Property(x => x.PostId)
            .HasConversion(id => id.Value, id => new PostId(id))
            .HasColumnName("PostId")
            .HasColumnType("uuid");

        builder.HasOne(x => x.Post)
            .WithMany("_reactions")
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(x => x.PostId);
        
        builder.HasOne(x => x.User)
            .WithMany("_postReactions")
            .OnDelete(DeleteBehavior.NoAction)  //todo cascade?
            .HasForeignKey(x => x.UserId);

        builder.ToTable("PostUserReaction");
    }
}

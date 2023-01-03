using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entity;

namespace Post.Infrastructure.EF.Config.WritingConfig;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(typeof(string), "_firstName")
            .HasColumnName("FirstName");

        builder.Property(typeof(string), "_lastName")
            .HasColumnName("LastName");

        builder.Property(typeof(string), "_email")
            .HasColumnName("EmailAddress");

        builder.HasMany("_posts")
            .WithOne("_author");

        builder.HasMany("_comments")
            .WithOne("_author");

        builder.HasMany(typeof(PostUserReaction), "_postReactions");

        builder.ToTable("User");
    }
}
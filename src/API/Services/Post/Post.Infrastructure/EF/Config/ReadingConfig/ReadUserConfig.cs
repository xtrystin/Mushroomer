using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Application.ReadModel;

namespace Post.Infrastructure.EF.Config.ReadingConfig;

public class ReadUserConfig : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(typeof(string), "_firstName")
            .HasColumnName("FirstName");

        builder.Property(typeof(string), "_lastName")
            .HasColumnName("LastName");

        builder.Property(x => x.Email)
            .HasColumnName("EmailAddress");

        builder.HasMany("_posts")
            .WithOne("Author");

        builder.HasMany("_comments")
            .WithOne("Author");

        builder.ToTable("User");
    }
}

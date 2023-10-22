using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.Email)
            .HasColumnName("EmailAddress");

        builder.HasMany(typeof(WarningUserReaction), "_warningReactions");

        builder.HasMany("_warnings").WithOne("Author");

        builder.ToTable("User");
    }
}

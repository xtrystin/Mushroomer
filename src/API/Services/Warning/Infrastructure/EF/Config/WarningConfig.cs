using Domain;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Config;

public class WarningConfig : IEntityTypeConfiguration<Warning>
{
    public void Configure(EntityTypeBuilder<Warning> builder)
    {
        builder.HasKey(x => x.Id);

        //builder.Property(x => x.Title).IsRequired().HasColumnName("Title");
        //builder.Property(x => x.Description).HasColumnName("Description");


        builder.HasMany(typeof(WarningUserReaction), "_reactions");

        builder.ToTable("Warning");
    }
}

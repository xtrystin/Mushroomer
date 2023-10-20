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

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("varchar(200)");

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("text");

        builder.Property(x => x.Province)
            .HasColumnName("Province")
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.Property(x => x.MushroomName)
            .HasColumnName("MushroomName")
            .HasColumnType("varchar(100)");

        builder.Property(x => x.Latitude)
            .HasColumnName("Latitude")
            .HasColumnType("double precision");

        builder.Property(x => x.Longitude)
            .HasColumnName("Longitude")
            .HasColumnType("double precision");

        builder.Property(x => x.Date)
            .HasColumnName("Date")
            .HasColumnType("timestamp without time zone");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnName("IsActive")
            .HasColumnType("boolean");

        builder.HasMany(typeof(WarningUserReaction), "_reactions");

        builder.ToTable("Warning");
    }
}

using Microsoft.EntityFrameworkCore;

namespace WebApplication1.EF;

public class MushroomConfig : IEntityTypeConfiguration<Mushroom.API.Model.Mushroom>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Mushroom.API.Model.Mushroom> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)");

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("varchar(2000)");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate")
            .HasColumnType("timestamp without time zone");

        builder.Property(x => x.IsPoisonous)
            .HasColumnName("IsPoisonous")
            .HasColumnType("boolean");
       
        builder.Property(x => x.PhotoUrl)
            .HasColumnName("PhotoUrl")
            .HasColumnType("varchar(300)")
            .IsRequired(false);

        builder.ToTable("Mushroom");
    }
}
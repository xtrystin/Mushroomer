using Microsoft.EntityFrameworkCore;

namespace WebApplication1.EF;

public class MushroomConfig : IEntityTypeConfiguration<Mushroom.API.Model.Mushroom>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Mushroom.API.Model.Mushroom> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("newsequentialid()");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(50)");

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("nvarchar(2000)");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate");

        builder.Property(x => x.IsPoisonous)
            .HasColumnName("IsPoisonous");
       
        builder.Property(x => x.PhotoUrl)
            .HasColumnName("PhotoUrl")
            .IsRequired(false);

        builder.ToTable("Mushroom");
    }
}
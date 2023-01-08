using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Application.ReadModel;
using User.Domain.ValueObject;

namespace User.Infrastructure.Ef.Config.ReadConfig;

public class ReadUserConfig : IEntityTypeConfiguration<UserReadModel>
{
    public void Configure(EntityTypeBuilder<UserReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id");

        builder.Property(x => x.FirstName)
            .HasColumnName("FirstName");

        builder.Property(x => x.LastName)
            .HasColumnName("LastName");

        builder.Property(x => x.EmailAddress)
            .HasColumnName("EmailAddress");

        builder.Property(x => x.ProfileDescription)
            .IsRequired(false)
            .HasColumnName("ProfileDescription");

        builder.Property(x => x.PhotoUrl)
            .IsRequired(false)
            .HasColumnName("PhotoUrl");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate");

        builder.ToTable("User");
    }
}

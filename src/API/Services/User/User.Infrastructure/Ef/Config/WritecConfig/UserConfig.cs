using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User.Domain.Entity;
using User.Domain.ValueObject;

namespace User.Infrastructure.Ef.Config.WritecConfig;

public class UserConfig : IEntityTypeConfiguration<Domain.Entity.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.User> builder)
    {
        builder.HasKey(x => x.Id);

        var firstNameConverter = new ValueConverter<FirstName, string>(firstname => firstname.Value,
            value => new FirstName(value));

        var lastNameConverter = new ValueConverter<LastName, string>(lastname => lastname.Value,
            value => new LastName(value));

        var emailAddressConverter = new ValueConverter<EmailAddress, string>(email => email.Value,
            value => new EmailAddress(value));

        var profileDescriptionConverter = new ValueConverter<ProfileDescription, string>(profileDescription => profileDescription.Value,
            value => new ProfileDescription(value));

        var photoUrlConverter = new ValueConverter<PhotoUrl, string>(photoUrl => photoUrl.Value,
            value => new PhotoUrl(value));

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new UserId(value))
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(typeof(FirstName), "_firstName")
            .IsRequired()
            .HasConversion(firstNameConverter)
            .HasColumnName("FirstName")
            .HasColumnType("varchar(100)");

        builder.Property(typeof(LastName), "_lastName")
            .IsRequired()
            .HasConversion(lastNameConverter)
            .HasColumnName("LastName")
            .HasColumnType("varchar(100)");

        builder.Property(typeof(EmailAddress), "_emailAddress")
            .IsRequired()
            .HasConversion(emailAddressConverter)
            .HasColumnName("EmailAddress")
            .HasColumnType("varchar(100)");

        builder.Property(typeof(ProfileDescription), "_profileDescription")
            .HasConversion(profileDescriptionConverter)
            .IsRequired(false)
            .HasColumnName("ProfileDescription")
            .HasColumnType("varchar(1000)");

        builder.Property(typeof(PhotoUrl), "_photoUrl")
            .HasConversion(photoUrlConverter)
            .IsRequired(false)
            .HasColumnName("PhotoUrl")
            .HasColumnType("varchar(200)");

        builder.Property(typeof(DateTime), "_createdDate")
            .IsRequired()
            .HasColumnType("timestamp without time zone")
            .HasColumnName("CreatedDate");

        builder.ToTable("User");
    }
}
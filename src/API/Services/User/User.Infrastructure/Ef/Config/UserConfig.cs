using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User.Domain.ValueObject;

namespace User.Infrastructure.Ef.Config;

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

        builder.Property(x => x.Id)
            .HasConversion(id => id.Value, value => new UserId(value));

        builder.Property(typeof(FirstName), "_firstName")
            .HasConversion(firstNameConverter)
            .HasColumnName("FirstName")
            .HasColumnType("nvarchar(100)");
        
        builder.Property(typeof(LastName), "_lastName")
            .HasConversion(lastNameConverter)
            .HasColumnName("LastName")
            .HasColumnType("nvarchar(100)");
        
        builder.Property(typeof(EmailAddress), "_emailAddress")
            .HasConversion(emailAddressConverter)
            .HasColumnName("EmailAddress")
            .HasColumnType("nvarchar(100)");

        builder.ToTable("User");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entity;
using User.Domain.ValueObject;

namespace User.Infrastructure.Ef.Config.WritecConfig;

public class UserFriendConfig : IEntityTypeConfiguration<UserFriend>
{
    public void Configure(EntityTypeBuilder<UserFriend> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired(false)                                              //todo 223: this is hack for problems with deleting row in many to self many relation, EF wants to set null to foreign key column isntead of deleting the whole row
            .HasColumnName("UserId");

        builder.Property(x => x.FriendId)
            .HasConversion(id => id.Value, value => new UserId(value))
            .IsRequired(false)                                             //todo 223: this is hack for problems with deleting row in many to self many relation, EF wants to set null to foreign key column isntead of deleting the whole row
            .HasColumnName("FriendId");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate");

        builder.HasOne(x => x.User)
            .WithMany("_friends")
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Friend)
            .WithMany("_friendToUsers")
            .HasForeignKey(x => x.FriendId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("UserFriend");
    }
}

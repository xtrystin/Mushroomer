using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Application.ReadModel;

namespace User.Infrastructure.Ef.Config.ReadConfig;

public class ReadUserFriendConfig : IEntityTypeConfiguration<UserFriendReadModel>
{
    public void Configure(EntityTypeBuilder<UserFriendReadModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .HasColumnName("UserId");

        builder.Property(x => x.FriendId)
            .HasColumnName("FriendId");

        builder.Property(x => x.CreatedDate)
            .HasColumnName("CreatedDate");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Friends)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Friend)
            .WithMany(x => x.FriendToUsers)
            .HasForeignKey(x => x.FriendId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("UserFriend");
    }
}

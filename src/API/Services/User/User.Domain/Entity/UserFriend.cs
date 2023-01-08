using User.Domain.ValueObject;

namespace User.Domain.Entity;

public class UserFriend
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public UserId UserId { get; set; }
    public UserId FriendId { get; set; }

    public User User { get; set; }
    public User Friend{ get; set; }
}

using User.Domain.ValueObject;

namespace User.Application.ReadModel;

public class UserFriendReadModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }

    public UserReadModel User { get; set; }
    public UserReadModel Friend { get; set; }
}

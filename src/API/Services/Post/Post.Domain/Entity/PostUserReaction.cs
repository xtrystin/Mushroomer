using Post.Domain.ValueObject;

namespace Post.Domain.Entity;

public class PostUserReaction
{
    public Guid Id { get; set; }
    public bool Like { get; set; } // todo: enum to have more reactions: happy, sad, like, dislikehttps://stackoverflow.com/questions/1434298/sql-server-equivalent-to-mysql-enum-data-type

    public User User { get; set; }
    public Guid UserId { get; set; }
    public Post Post { get; set; }
    public PostId PostId { get; set; }
}

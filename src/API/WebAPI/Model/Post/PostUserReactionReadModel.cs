using System.Text.Json.Serialization;

namespace WebAPI.Model.Post;

public class PostUserReactionReadModel
{
    public Guid Id { get; set; }
    public bool Like { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}

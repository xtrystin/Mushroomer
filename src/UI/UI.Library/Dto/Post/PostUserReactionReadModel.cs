using System.Text.Json.Serialization;

namespace UI.ApiLibrary.Dto.Post;

public class PostUserReactionReadModel
{
    public Guid Id { get; set; }
    public bool Like { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}

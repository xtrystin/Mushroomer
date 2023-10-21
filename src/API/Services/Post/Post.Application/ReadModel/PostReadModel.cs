using Post.Application.ReadModel;
using System.Text.Json.Serialization;

namespace Post.Application.Dto;

public class PostReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public bool IsActive { get; set; }
    public string AuthorEmail => Author?.Email;
    public int LikeNumber => Reactions.Count(x => x.Like == true);
    public int DisLikeNumber => Reactions.Count(x => x.Like == false);
    public string? ThumbnailPhotoUrl { get; set; }

    [JsonIgnore]
    public IEnumerable<CommentReadModel> Comments { get; set; }

    [JsonIgnore]
    public UserReadModel Author { get; set; }

    [JsonIgnore]
    public List<PostUserReactionReadModel> Reactions { get; set; }
}

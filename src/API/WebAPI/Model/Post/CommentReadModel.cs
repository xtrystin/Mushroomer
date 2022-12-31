using System.Text.Json.Serialization;

namespace WebAPI.Model.Post;

public class CommentReadModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }

    [JsonIgnore]
    public Guid PostId { get; set; }
    [JsonIgnore]
    public PostReadModel Post { get; set; }
}

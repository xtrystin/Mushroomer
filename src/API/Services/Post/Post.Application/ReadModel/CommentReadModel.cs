using System.Text.Json.Serialization;

namespace Post.Application.Dto;

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

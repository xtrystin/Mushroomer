using System.Text.Json.Serialization;

namespace Post.Application.Dto;

public class PostReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    
    [JsonIgnore]
    public IEnumerable<CommentReadModel> Comments { get; set; }
}

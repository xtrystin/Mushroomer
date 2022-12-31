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
    public string AuthorEmail => Author.Email;

    [JsonIgnore]
    public IEnumerable<CommentReadModel> Comments { get; set; }

    [JsonIgnore]
    public UserReadModel Author { get; set; }
}

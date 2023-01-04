using System.Text.Json.Serialization;

namespace WebAPI.Model.Post;

public class CommentReadModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public string AuthorEmail { get; set; }

}

using System.Text.Json.Serialization;

namespace UI.ApiLibrary.Dto.Post;

public class CommentReadModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public string AuthorEmail { get; set; }
    public Guid AuthorId { get; set; }
    public Guid PostId { get; set; }
}

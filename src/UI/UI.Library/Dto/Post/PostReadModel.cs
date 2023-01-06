using System.Text.Json.Serialization;

namespace UI.ApiLibrary.Dto.Post;

public class PostReadModel      //todo refactor: public/private
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public string AuthorEmail { get; set; }
    public int LikeNumber { get; set; }
    public int DisLikeNumber { get; set; }
}

namespace WebAPI.Model.Post;

public class PostReadModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public bool IsActive { get; set; }
    public string AuthorEmail {get; set; }
    public int LikeNumber { get; set; }
    public int DisLikeNumber { get; set; }
}

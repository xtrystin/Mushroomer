namespace Post.Application.Dto;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModificationDate { get; set; }
    public string AuthorEmail { get; set; }
    public Guid CommentAuthorId { get; set; }
    public Guid PostId { get; set; }
    public string PostTitle { get; set; }
    public string PostAuthorEmail { get; set; } 
}

namespace UI.ApiLibrary.Dto.Post;

public class AddCommentDto
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
}

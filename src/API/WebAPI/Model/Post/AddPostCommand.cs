using MediatR;

namespace WebAPI.Model.Post;

public class AddPostCommand
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid AuthorId { get; set; }
}

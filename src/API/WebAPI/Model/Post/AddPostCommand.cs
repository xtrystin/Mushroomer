using MediatR;

namespace WebAPI.Model.Post;

public class AddPostCommand : IRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}

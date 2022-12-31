using MediatR;

namespace WebAPI.Model.Post;

public class AddCommentCommand : IRequest
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
}

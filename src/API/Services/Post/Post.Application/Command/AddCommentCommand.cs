using MediatR;

namespace Post.Application.Command;

public class AddCommentCommand : IRequest
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
}

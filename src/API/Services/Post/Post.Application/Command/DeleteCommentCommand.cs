using MediatR;

namespace Post.Application.Command;

public class DeleteCommentCommand : IRequest
{
    public Guid PostId { get; set;}
    public Guid CommentId { get; set;}
}

using MediatR;

namespace Post.Application.Command;

public class ModifyCommentCommand : IRequest
{
    public Guid PostId { get; set; }
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
}

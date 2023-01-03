using MediatR;

namespace Post.Application.Command;

public class AddReactionToPostCommand : IRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public bool Like { get; set; }
}

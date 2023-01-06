using MediatR;

namespace Post.Application.Command;

public class DeletePostCommand : IRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}

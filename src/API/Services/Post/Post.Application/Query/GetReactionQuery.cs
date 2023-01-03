using MediatR;

namespace Post.Application.Query;

public class GetReactionQuery : IRequest<bool?>
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}

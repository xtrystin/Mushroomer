using MediatR;

namespace Application.Queries;

public class GetReactionQuery : IRequest<bool?>
{
    public Guid WarningId { get; set; }
    public Guid UserId { get; set; }
}

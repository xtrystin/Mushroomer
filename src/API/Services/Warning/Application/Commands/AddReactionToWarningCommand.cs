using MediatR;

namespace Application.Commands;

public class AddReactionToWarningCommand : IRequest
{
    public Guid WarningId { get; set; }
    public Guid UserId { get; set; }
    public bool Approve { get; set; }
}

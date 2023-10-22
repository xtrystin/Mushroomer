using MediatR;

namespace Application.Commands;
public class ChangeLocationStatusCommand : IRequest
{
    public Guid LocationId { get; set; }
    public bool ChangeToActive { get; set; }
}

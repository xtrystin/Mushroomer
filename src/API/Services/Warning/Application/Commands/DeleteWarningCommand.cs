using Application.Dto;
using MediatR;

namespace Application.Commands;

public class DeleteWarningCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool IsUserMod { get; set; }
}

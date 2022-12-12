using Application.Dto;
using MediatR;

namespace Application.Commands;

public class UpdateWarningCommand : IRequest
{
    public WarningDto Warning { get; set; }
}

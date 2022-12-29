using Application.Dto;
using MediatR;

namespace Application.Commands;

public class AddWarningCommand : IRequest
{
    public WarningDto Warning { get; set; }
}

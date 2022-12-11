using Application.Dto;
using MediatR;

namespace Application.Queries;

public class GetWarningQuery : IRequest<WarningDto>
{
    public Guid Id { get; set; }        //todo: validation
}

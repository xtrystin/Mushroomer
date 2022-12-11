using Application.Dto;
using MediatR;

namespace Application.Queries;

public class GetAllWarningsQuery : IRequest<IEnumerable<WarningDto>>
{
}

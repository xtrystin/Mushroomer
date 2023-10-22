using Application.Dto;
using MediatR;

namespace Application.Queries;

public class GetAllWarningsQuery : IRequest<IEnumerable<WarningDto>>
{
    public bool OnlyInactive { get; set; }
    public bool OnlyInactiveForUser { get; set; }
    public string UserEmail { get; set; }
    public bool IsUserMod { get; set; }
}

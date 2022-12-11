using Application.Dto;
using Application.Queries;
using MediatR;

namespace Infrastructure.Dapper.Queries;

public class GetAllWarningsQueryHandler : IRequestHandler<GetAllWarningsQuery, IEnumerable<WarningDto>>
{
    async Task<IEnumerable<WarningDto>> IRequestHandler<GetAllWarningsQuery, IEnumerable<WarningDto>>.Handle(GetAllWarningsQuery request, CancellationToken cancellationToken)
    {
        List<WarningDto> output = WarningsInMemoryData.Warnings;

        return output;
        //throw new NotImplementedException();
    }
}

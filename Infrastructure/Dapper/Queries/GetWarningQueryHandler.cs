using Application.Dto;
using Application.Queries;
using MediatR;

namespace Infrastructure.Dapper.Queries;

public class GetWarningQueryHandler : IRequestHandler<GetWarningQuery, WarningDto>
{
    public async Task<WarningDto> Handle(GetWarningQuery request, CancellationToken cancellationToken)
    {
        WarningDto output = WarningsInMemoryData.Warnings.Where(x => x.Id == request.Id).First();
        //todoL if null return notFound
        return output;
    }
}

using Application.Dto;
using Application.Queries;
using Domain;
using Infrastructure.EF.Context;
using Infrastructure.EF.Mapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Queries;

public class GetWarningQueryHandler : IRequestHandler<GetWarningQuery, WarningDto>
{
    private readonly DbSet<Warning> _warnings;

    public GetWarningQueryHandler(WarningDbContext dbContext)
    {
        _warnings = dbContext.Warnings;
    }

    public async Task<WarningDto> Handle(GetWarningQuery request, CancellationToken cancellationToken)
    {
        var warning = await _warnings.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
        //todoL if null return notFound

        return WarningMapper.MapToDto(warning);
    }
}

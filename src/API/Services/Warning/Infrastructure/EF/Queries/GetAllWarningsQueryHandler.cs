using Application.Dto;
using Application.Queries;
using Domain.Entity;
using Infrastructure.EF.Context;
using Infrastructure.EF.Mapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Queries;

public class GetAllWarningsQueryHandler : IRequestHandler<GetAllWarningsQuery, IEnumerable<WarningDto>>
{
    private DbSet<Warning> _warnings;

    public GetAllWarningsQueryHandler(WarningDbContext dbContext)
    {
        _warnings = dbContext.Warnings;
    }

    public async Task<IEnumerable<WarningDto>> Handle(GetAllWarningsQuery request, CancellationToken cancellationToken)
    {
        var warnings = await _warnings.Include(x => x._reactions).Select(x => WarningMapper.MapToDto(x)).ToListAsync();
        return warnings;
    }
}

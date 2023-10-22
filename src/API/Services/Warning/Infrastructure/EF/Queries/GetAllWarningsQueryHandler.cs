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
        if (request.OnlyInactiveForUser && string.IsNullOrEmpty(request.UserEmail) is false)
            return await _warnings.Where(x => x.Author.Email == request.UserEmail && x.IsActive == false)
               .Include(x => x._reactions).Include(x => x.Author).Select(x => WarningMapper.MapToDto(x)).ToListAsync();
        else if (request.OnlyInactive && request.IsUserMod)
            return await _warnings.Where(x => x.IsActive == false).Include(x => x._reactions)
                .Include(x => x.Author).Select(x => WarningMapper.MapToDto(x)).ToListAsync();
        else
            return await _warnings.Where(x => x.IsActive).Include(x => x._reactions)
                .Include(x => x.Author).Select(x => WarningMapper.MapToDto(x)).ToListAsync();
    }
}

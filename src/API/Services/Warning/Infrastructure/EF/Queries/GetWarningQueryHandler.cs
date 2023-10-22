using Application.Dto;
using Application.Exception;
using Application.Queries;
using Domain.Entity;
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
        var warning = await _warnings.Include(x => x._reactions).Include(x => x.Author).Where(x => x.Id == request.Id).FirstOrDefaultAsync();
        
        if (warning != null && warning.IsActive)
            return WarningMapper.MapToDto(warning);
        else if (warning != null && warning.IsActive == false &&
            (request.UserId != null && warning.Author.Id == request.UserId) || request.IsUserMod)
            return WarningMapper.MapToDto(warning);
        else
            throw new NotAuthorizedForOperation("You are not authorized to view this location");
        
    }
}

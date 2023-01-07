using Application.Queries;
using Infrastructure.EF.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Queries;

public class GetReactionQueryHandler : IRequestHandler<GetReactionQuery, bool?>
{
    private readonly WarningDbContext _dbContext;

    public GetReactionQueryHandler(WarningDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool?> Handle(GetReactionQuery request, CancellationToken cancellationToken)
    {
        var reaction = _dbContext.Warnings.Include(x => x._reactions).FirstOrDefault(x => x.Id == request.WarningId)
            ._reactions.FirstOrDefault(x => x.WarningId == request.WarningId && x.UserId == request.UserId);

        bool? output = reaction is null ? null : reaction.Approve;

        return output;
    }
}

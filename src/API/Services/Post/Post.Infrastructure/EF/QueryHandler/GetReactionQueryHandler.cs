using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Query;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.QueryHandler;

public class GetReactionQueryHandler : IRequestHandler<GetReactionQuery, bool?>
{
    private readonly ReadPostDbContext _dbContext;

    public GetReactionQueryHandler(ReadPostDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool?> Handle(GetReactionQuery request, CancellationToken cancellationToken)
    {
        var reaction = _dbContext.Posts.Include(x => x.Reactions).FirstOrDefault(x => x.Id == request.PostId)
            .Reactions.FirstOrDefault(x => x.PostId == request.PostId && x.UserId == request.UserId);

        bool? output = reaction is null ? null : reaction.Like;

        return output;
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.Query;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.QueryHandler;

public class GetCommentsForPostQueryHandler : IRequestHandler<GetCommentsForPostQuery, IEnumerable<CommentReadModel>>
{
    private readonly ReadPostDbContext _dbReadContext;

    public GetCommentsForPostQueryHandler(ReadPostDbContext dbReadContext)
    {
        _dbReadContext = dbReadContext;
    }

    public async Task<IEnumerable<CommentReadModel>> Handle(GetCommentsForPostQuery request, CancellationToken cancellationToken)
    {
        var a =
        (await _dbReadContext.Posts.Where(x => x.Id == request.PostId).
            Include(x => x.Author).Include(x => x.Comments).ThenInclude(x => x.Author).FirstAsync()).Comments;
        return a;
    }
}
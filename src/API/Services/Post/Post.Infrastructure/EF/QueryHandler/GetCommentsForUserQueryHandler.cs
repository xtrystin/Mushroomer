using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.Query;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.QueryHandler;

public class GetCommentsForUserQueryHandler : IRequestHandler<GetCommentsForUserQuery, IEnumerable<CommentReadModel>>
{
    private readonly ReadPostDbContext _dbReadContext;

    public GetCommentsForUserQueryHandler(ReadPostDbContext dbReadContext)
    {
        _dbReadContext = dbReadContext;
    }

    public async Task<IEnumerable<CommentReadModel>> Handle(GetCommentsForUserQuery request, CancellationToken cancellationToken)
    {
        var a = await _dbReadContext.Comments.Include(x => x.Author).Where(x => x.Author.Id == request.UserId).ToListAsync();
        return a;
    }
}
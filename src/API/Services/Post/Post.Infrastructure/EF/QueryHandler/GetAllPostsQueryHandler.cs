using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.Query;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.QueryHandler;

public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostReadModel>>
{
    private readonly ReadPostDbContext _dbReadContext;

    public GetAllPostsQueryHandler(ReadPostDbContext dbReadContext)
    {
        _dbReadContext = dbReadContext;
    }
    public async Task<IEnumerable<PostReadModel>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        if (request.OnlyInactiveForUser && string.IsNullOrEmpty(request.UserEmail) is false)
            return await _dbReadContext.Posts.Where(x => x.Author.Email == request.UserEmail && x.IsActive == false)
                .Include(x => x.Author).Include(x => x.Reactions).ToListAsync();
        else if (request.OnlyInactive && request.IsUserMod)
            return await _dbReadContext.Posts.Where(x => x.IsActive == false).Include(x => x.Author).Include(x => x.Reactions).ToListAsync();
        else
            return await _dbReadContext.Posts.Where(x => x.IsActive).Include(x => x.Author).Include(x => x.Reactions).ToListAsync();
    }
}

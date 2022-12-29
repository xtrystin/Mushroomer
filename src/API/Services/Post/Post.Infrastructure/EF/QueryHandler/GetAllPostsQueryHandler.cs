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
        => await _dbReadContext.Posts.ToListAsync();
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.Query;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.Query
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostReadModel>
    {
        private readonly ReadPostDbContext _dbReadContext;

        public GetPostQueryHandler(ReadPostDbContext dbReadContext)
        {
            _dbReadContext = dbReadContext;
        }

        public async Task<PostReadModel> Handle(GetPostQuery request, CancellationToken cancellationToken)    // todo: handle null?
            => await _dbReadContext.Posts.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == request.Id);
    }
}

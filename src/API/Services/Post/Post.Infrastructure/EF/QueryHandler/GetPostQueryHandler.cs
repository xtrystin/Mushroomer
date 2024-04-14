using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.Exception;
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
        {
            var post = await _dbReadContext.Posts.Include(x => x.Author).Include(x => x.Reactions)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (post is null)
                throw new PostNotFoundException();
            
            if (post != null && post.IsActive)
                return post;
            else if (post is not null && post.IsActive is false &&
                ((request.UserId != null && post.Author.Id == request.UserId) || request.IsUserMod))
                return post;
            else
                throw new NotAuthorizedToViewInactivePostException("You are not authorized to view this post");

        }
    }
}

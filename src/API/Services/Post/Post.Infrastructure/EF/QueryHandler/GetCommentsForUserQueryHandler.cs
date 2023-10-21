using MediatR;
using Microsoft.EntityFrameworkCore;
using Post.Application.Dto;
using Post.Application.Query;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.QueryHandler;

public class GetCommentsForUserQueryHandler : IRequestHandler<GetCommentsForUserQuery, IEnumerable<CommentDto>>
{
    private readonly ReadPostDbContext _dbReadContext;

    public GetCommentsForUserQueryHandler(ReadPostDbContext dbReadContext)
    {
        _dbReadContext = dbReadContext;
    }

    public async Task<IEnumerable<CommentDto>> Handle(GetCommentsForUserQuery request, CancellationToken cancellationToken)
    {
        var q = _dbReadContext.Comments.Where(c => c.Author.Id == request.UserId)
            .Select(s => new CommentDto
            {
                Id = s.Id,
                PostId = s.PostId,
                AuthorEmail = s.Author.Email,
                CommentAuthorId = s.Author.Id,
                Content = s.Content,
                CreatedDate = s.CreatedDate,
                LastModificationDate = s.LastModificationDate,
                PostAuthorEmail = s.Post.Author.Email,
                PostTitle = s.Post.Title
            });

        return await q.ToListAsync();
    }
}
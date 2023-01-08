using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetCommentsForUserQuery : IRequest<IEnumerable<CommentReadModel>>
{
    public Guid UserId { get; set; }
}

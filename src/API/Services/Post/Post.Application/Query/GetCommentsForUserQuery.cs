using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetCommentsForUserQuery : IRequest<IEnumerable<CommentDto>>
{
    public Guid UserId { get; set; }
}

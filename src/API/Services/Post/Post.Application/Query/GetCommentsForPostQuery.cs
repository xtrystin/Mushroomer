using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetCommentsForPostQuery : IRequest<IEnumerable<CommentReadModel>>
{
    public Guid PostId { get; set; }
}

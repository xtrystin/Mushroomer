using MediatR;

namespace WebAPI.Model.Post;

public class GetCommentsForPostQuery : IRequest<IEnumerable<CommentReadModel>>
{
    public Guid PostId { get; set; }
}

using MediatR;

namespace WebAPI.Model.Post;

public class GetAllPostsQuery : IRequest<IEnumerable<PostReadModel>>
{
}

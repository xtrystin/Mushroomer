using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetAllPostsQuery : IRequest<IEnumerable<PostReadModel>>
{
}

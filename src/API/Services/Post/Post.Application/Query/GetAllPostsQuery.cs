using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetAllPostsQuery : IRequest<IEnumerable<PostReadModel>>
{
    public bool OnlyInactive { get; set; }
    public bool IsUserMod { get; set; }
    public bool OnlyInactiveForUser { get; set; }
    public string UserEmail { get; set; }
}

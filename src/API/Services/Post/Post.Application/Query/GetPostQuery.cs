using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetPostQuery : IRequest<PostReadModel>
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public bool IsUserMod { get; set; }
}

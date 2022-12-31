using MediatR;

namespace WebAPI.Model.Post;

public class GetPostQuery : IRequest<PostReadModel>
{
    public Guid Id { get; set; }
}

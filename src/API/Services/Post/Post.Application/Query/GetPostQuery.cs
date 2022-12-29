using MediatR;
using Post.Application.Dto;

namespace Post.Application.Query;

public class GetPostQuery : IRequest<PostReadModel>
{
    public Guid Id { get; set; }
}

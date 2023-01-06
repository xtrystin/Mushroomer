using MediatR;
using Post.Application.Dto;

namespace Post.Application.Command;

public class AddPostCommand : IRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid AuthorId { get; set; }
}

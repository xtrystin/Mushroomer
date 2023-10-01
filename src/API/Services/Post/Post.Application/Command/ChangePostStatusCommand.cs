using MediatR;

namespace Post.Application.Command;
public class ChangePostStatusCommand : IRequest
{
    public Guid PostId { get; set; }
    public bool ChangeToActive { get; set; }
}

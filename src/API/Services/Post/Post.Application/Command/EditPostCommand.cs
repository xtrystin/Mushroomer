using MediatR;
using System.Text.Json.Serialization;

namespace Post.Application.Command;

public class EditPostCommand : IRequest
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public string? ThumbnailPhotoUrl { get; set; }
}

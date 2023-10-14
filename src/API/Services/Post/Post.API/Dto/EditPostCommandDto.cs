namespace Post.API.Dto;

public class EditPostCommandDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? ThumbnailPhotoUrl { get; set; }
}

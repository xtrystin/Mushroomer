namespace WebAPI.Model.Post;

public class AddPostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string? ThumbnailPhotoUrl { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace UI.ApiLibrary.Dto.Post;

public class AddPostDto
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Title can have max 200 characters")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; }

    [MaxLength(400, ErrorMessage = "Photo Url can have max 400 characters")]
    public string? ThumbnailPhotoUrl { get; set; }
}

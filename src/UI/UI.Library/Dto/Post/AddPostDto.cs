using System.ComponentModel.DataAnnotations;

namespace UI.ApiLibrary.Dto.Post;

public class AddPostDto
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(20, ErrorMessage = "Title can have max 20 characters")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; }
}

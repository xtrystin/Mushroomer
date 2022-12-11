using System.ComponentModel.DataAnnotations;

namespace WebAssemblyUI.Models;

public class UserCredentialsModel
{
    [Required(ErrorMessage = "Email Address is required")]  //todo validation?
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}

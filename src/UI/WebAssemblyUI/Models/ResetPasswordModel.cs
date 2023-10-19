using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAssemblyUI.Models;

public class ResetPasswordModel
{
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    [Required]
    [DisplayName("Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; }

    public string Token { get; set; }
}

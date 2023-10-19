using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AuthService.Models;

public class ResetPasswordModel
{
    [Required]
    [EmailAddress]
    [DisplayName("Email Address")]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    [Required]
    [DisplayName("Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Token { get; set; }
}

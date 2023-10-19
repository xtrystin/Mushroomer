using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AuthService.Models;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    [DisplayName("Email Address")]
    public string Email { get; set; }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAssemblyUI.Models;

public class RegisterModel
{
    [Required]
    [DisplayName("First Name")]
    public string FirstName { get; set; }

    [Required]
    [DisplayName("Last Name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [DisplayName("Email Address")]
    public string EmailAddress { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [DisplayName("Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; }
}

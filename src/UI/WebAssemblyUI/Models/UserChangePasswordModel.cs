namespace WebAssemblyUI.Models;

public class UserChangePasswordModel
{
    public string UserId { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

using WebAssemblyUI.Contracts;
using WebAssemblyUI.Models;

namespace WebAssemblyUI.Authentication
{
    public interface IAuthenticationService
    {
        Task Login(UserCredentialsModel userCredentials);
        Task Logout();
        Task Register(RegisterModel model);
    }
}

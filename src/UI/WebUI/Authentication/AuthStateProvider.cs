using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WebUI.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(
                new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity())));
            
            
        }
    }
}

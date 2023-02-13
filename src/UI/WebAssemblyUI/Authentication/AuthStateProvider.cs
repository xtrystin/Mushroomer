using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace WebAssemblyUI.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;
    private readonly string _bearerTokenStorageKey;

    public AuthStateProvider(HttpClient httpClient,
                             ILocalStorageService localStorage,
                             IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        _bearerTokenStorageKey = _config["bearerTokenStorageKey"];
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(_bearerTokenStorageKey);
        if (string.IsNullOrWhiteSpace(token) || HasTokenExpired(token))
        {
            return _anonymous;
        }

        bool isAuthenticated = await NotifyUserAuthentication(token);
        if (isAuthenticated == false)
        {
            return _anonymous;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(
            new ClaimsPrincipal(
                new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),
                "jwtAuthType")));
    }

    private bool HasTokenExpired(string token)
    {
        List<Claim> claims = JwtParser.ParseClaimsFromJwt(token).ToList();
        var exp = long.Parse(claims.First(claim => claim.Type.Equals("exp")).Value);
        
        var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
        var now = DateTime.Now.ToUniversalTime();

        var hasExpired = expDate <= now;
        return hasExpired;
    }

    public async Task<bool> NotifyUserAuthentication(string token)
    {
        bool isAuthenticatedOutput;
        Task<AuthenticationState> authState;
        try
        {
            //await _apiHelper.GetLoggedInUserInfo(token);

            var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),
                 "jwtAuthType"));

            authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
            isAuthenticatedOutput = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await NotifyUserLogout();
            isAuthenticatedOutput = false;
        }

        return isAuthenticatedOutput;
    }

    public async Task NotifyUserLogout()
    {
        await _localStorage.RemoveItemAsync(_bearerTokenStorageKey);

        //_apiHelper.LogOffUser();
        _httpClient.DefaultRequestHeaders.Authorization = null;

        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}

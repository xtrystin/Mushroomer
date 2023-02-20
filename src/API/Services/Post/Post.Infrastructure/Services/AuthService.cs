using Microsoft.Extensions.Configuration;
using Post.Application.Service;
using System.Net.Http.Json;

namespace Post.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuthService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<bool> IsUserInRole(Guid userId, string roleName)
    {
        string url = _config["ExternalServices:AuthService"] + $"/User/{userId}/IsInRole?roleName={roleName}";    
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }
        else
        {
            throw new AuthServiceException();
        }
    }
}

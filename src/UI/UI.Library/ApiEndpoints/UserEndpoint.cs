using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using UI.ApiLibrary.Dto.User;

namespace UI.ApiLibrary.ApiEndpoints;

public class UserEndpoint : IUserEndpoint
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private string _api;

    public UserEndpoint(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _api = _config["apiGateway"];
    }

    public async Task<UserReadModel> Get(Guid id)
    {
        var url = _api + $"/api/user/{id}";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<UserReadModel>();
            return result;
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<IEnumerable<UserReadModel>> GetAll()
    {
        var url = _api + $"/api/user";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserReadModel>>();
            return result;
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ChangeFriendship(Guid friendId, bool add)
    {
        var url = _api + $"/api/user/friend?add={add}";

        var response = await _httpClient.PostAsJsonAsync(url, add);
        if (response.IsSuccessStatusCode)
        {
            // log success
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }


}

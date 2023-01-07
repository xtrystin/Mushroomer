using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UI.ApiLibrary.Dto.Mushroom;

namespace UI.ApiLibrary.ApiEndpoints;

public class MushroomEndpoint : IMushroomEndpoint
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private string _api;

    public MushroomEndpoint(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _api = _config["apiGateway"];
    }

    public async Task<List<Mushroom>> GetAll()
    {
        string apiEndpoint = _api + "/api/mushroom";

        using var response = await _httpClient.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<Mushroom>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<Mushroom> Get(string id)
    {
        string apiEndpoint = _api + $"/api/mushroom/{id}";

        using var response = await _httpClient.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Mushroom>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task Add(MushroomDto mushroom)
    {
        string apiEndpoint = _api + $"/api/mushroom";

        using var response = await _httpClient.PostAsJsonAsync(apiEndpoint, mushroom);
        if (response.IsSuccessStatusCode)
        {
            //log success
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task Delete(Guid id)
    {
        string apiEndpoint = _api + $"/api/mushroom/{id}";

        using var response = await _httpClient.DeleteAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            //log success
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task Update(Mushroom mushroom)
    {
        string apiEndpoint = _api + $"/api/mushroom/{mushroom.Id}";
        MushroomDto mushroomDto = new() { Name = mushroom.Name, Description = mushroom.Description,
            IsPoisonous = mushroom.IsPoisonous, PhotoUrl = mushroom.PhotoUrl };

        var stringContent = new StringContent(JsonSerializer.Serialize(mushroomDto), Encoding.UTF8, "application/json");

        using var response = await _httpClient.PatchAsync(apiEndpoint, stringContent);
        if (response.IsSuccessStatusCode)
        {
            //log success
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}

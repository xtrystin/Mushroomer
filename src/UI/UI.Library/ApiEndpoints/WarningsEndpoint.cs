using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using UI.ApiLibrary.Dto;

namespace UI.ApiLibrary.ApiEndpoints;

public class WarningsEndpoint : IWarningsEndpoint
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private string _api;

    public WarningsEndpoint(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _api = _config["resourceApi"];
    }

    public async Task<List<WarningDto>> GetAll()
    {
        string apiEndpoint = _api + "/api/Warnings";

        using var response = await _httpClient.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<WarningDto>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<WarningDto> Get(string id)
    {
        string apiEndpoint = _api + $"/api/Warnings/{id}";

        using var response = await _httpClient.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<WarningDto>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task Update(WarningDto warning)
    {
        string apiEndpoint = _api + $"/api/Warnings";

        using var response = await _httpClient.PutAsJsonAsync(apiEndpoint, warning);
        if (response.IsSuccessStatusCode)
        {
            //log success
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task Add(WarningDto warning)
    {
        string apiEndpoint = _api + $"/api/Warnings";

        using var response = await _httpClient.PostAsJsonAsync(apiEndpoint, warning);
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
        string apiEndpoint = _api + $"/api/Warnings/{id}";

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
}

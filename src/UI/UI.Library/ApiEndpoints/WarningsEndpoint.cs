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
        _api = _config["apiGateway"];
    }

    public async Task<List<WarningDto>> GetAll(bool onlyInactive = false, bool onlyInactiveForUser = false)
    {
        string apiEndpoint = _api + $"/api/Warnings?onlyInactive={onlyInactive}&onlyInactiveForUser={onlyInactiveForUser}";

        using var response = await _httpClient.GetAsync(apiEndpoint);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<WarningDto>>();
            return result;
        }
        else
        {
            var reasonContent = await response.Content.ReadAsStringAsync();
            throw new Exception(string.IsNullOrEmpty(reasonContent) ? response.ReasonPhrase : reasonContent);
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
            var reasonContent = await response.Content.ReadAsStringAsync();
            throw new Exception(string.IsNullOrEmpty(reasonContent) ? response.ReasonPhrase : reasonContent);
        }
    }

    public async Task Update(WarningDto warning)    //todo change warningDto to new model
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

    public async Task Add(WarningDto warning)       //todo change warningDto to new model
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

    public async Task ChangeStatus(Guid id, bool changeToActive)
    {
        var url = _api + $"/api/Warnings/{id}/changeStatus?changeToActive={changeToActive}";

        var response = await _httpClient.PatchAsync(url, default);
        if (response.IsSuccessStatusCode)
        {
            // log success
        }
        else
        {
            var reasonContent = await response.Content.ReadAsStringAsync();
            throw new Exception(string.IsNullOrEmpty(reasonContent) ? response.ReasonPhrase : reasonContent);
        }
    }

    public async Task<bool?> GetReactionForUser(Guid warningId, Guid userId)
    {
        var url = _api + $"/api/warnings/{warningId}/userReaction/{userId}";

        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<bool?>();
            return result;
        }
        else if (response.StatusCode is System.Net.HttpStatusCode.NoContent or System.Net.HttpStatusCode.Unauthorized)
        {
            return null;
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task PostReaction(Guid warningId, bool approve)
    {
        var url = _api + $"/api/warnings/{warningId}/reaction?approve={approve}";

        var response = await _httpClient.PostAsync(url, null);
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

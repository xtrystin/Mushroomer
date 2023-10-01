using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UI.ApiLibrary.Dto.Post;

namespace UI.ApiLibrary.ApiEndpoints;

public class PostEndpoint : IPostEndpoint
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private string _api;

    public PostEndpoint(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _api = _config["apiGateway"];
    }

    public async Task<PostReadModel> Get(Guid id)
    {
        var url = _api + $"/api/Post/{id}";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<PostReadModel>();
            return result;
        }
        else
        {
            var reasonContent = await response.Content.ReadAsStringAsync();
            throw new Exception(string.IsNullOrEmpty(reasonContent) ? response.ReasonPhrase : reasonContent);
        }
    }

    public async Task<IEnumerable<PostReadModel>> GetAll(bool onlyInactive = false, bool onlyInactiveForUser = false)
    {
        var url = _api + $"/api/Post?onlyInactive={onlyInactive}&onlyInactiveForUser={onlyInactiveForUser}";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<PostReadModel>>();
            return result;
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task Add(AddPostDto post)
    {
        var url = _api + $"/api/Post";

        var response = await _httpClient.PostAsJsonAsync(url, post);
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

    public async Task Edit(Guid id, EditPostCommandDto editPostDto)
    {
        var url = _api + $"/api/Post/{id}";
        var stringContent = new StringContent(JsonSerializer.Serialize(editPostDto), Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync(url, stringContent);
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

    public async Task Delete(Guid id)
    {
        var url = _api + $"/api/Post/{id}";

        var response = await _httpClient.DeleteAsync(url);
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

    public async Task ChangeStatus(Guid id, bool changeToActive)
    {
        var url = _api + $"/api/Post/{id}/changeStatus?changeToActive={changeToActive}";

        var response = await _httpClient.PatchAsync(url, default);
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

    public async Task<IEnumerable<CommentReadModel>> GetCommentsForPost(Guid postId)
    {
        var url = _api + $"/api/Post/{postId}/comment";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<CommentReadModel>>();
            return result;
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<IEnumerable<CommentReadModel>> GetCommentsForUser(Guid userId)
    {
        var url = _api + $"/api/Post/comment/user/{userId}";

        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<CommentReadModel>>();
            return result;
        }
        else
        {
            // log error
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task ModifyComment(Guid postId, string content, Guid commentId)
    {
        var url = _api + $"/api/Post/{postId}/comment/{commentId}";
        var stringContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");

        var response = await _httpClient.PatchAsync(url, stringContent);
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

    public async Task DeleteComment(Guid postId, Guid commentId)
    {
        var url = _api + $"/api/Post/{postId}/comment/{commentId}";

        var response = await _httpClient.DeleteAsync(url);
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

    public async Task AddCommentToPost(AddCommentDto comment)
    {
        var url = _api + $"/api/Post/{comment.PostId}/comment";

        var response = await _httpClient.PostAsJsonAsync(url, comment.Content);
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

    public async Task<bool?> GetReactionForUser(Guid postId)
    {
        var url = _api + $"/api/Post/{postId}/reactionForUser";

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

    public async Task PostReaction(Guid postId, bool like)
    {
        var url = _api + $"/api/Post/{postId}/reaction?like={like}";

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

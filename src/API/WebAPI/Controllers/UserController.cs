using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebAPI.Model.User;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public UserController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    private void AddJwtToHttpClientHeader()     // todo: replace with [FromHeader]
    {
        var jwtBearer = HttpContext.Request.Headers.Authorization;
        if (jwtBearer.Count == 0)
        {
            return;
        }

        var jwt = jwtBearer.ToString().Replace("bearer ", "");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt); // todo: use middleware to set authorization header
    }

    [HttpGet]
    public async Task<IEnumerable<UserReadModel>> GetAll()     //todo: do not return friends for getAll
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:User"] + $"/user";

        var response = await _httpClient.GetAsync(url);     //todo: dispatcher
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserReadModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<UserReadModel> Get([FromRoute] Guid id)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:User"] + $"/user/{id}";

        var response = await _httpClient.GetAsync(url);     //todo: dispatcher
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<UserReadModel>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpPost("Friend")]
    [EndpointDescription("Add or remove friend to/from friend list. Set add to true if you want to add. For removing set add to false")]
    public async Task<IActionResult> AddFriend([FromBody]Guid friendId, [FromQuery]bool add = true)
    {
        AddJwtToHttpClientHeader();
        
        var url = _config["MicroservicesUrl:User"] + $"/user/friend?add={add}";
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        ChangeFriendCommandDto payload = new() { UserId = userId, FriendId = friendId};

        var response = await _httpClient.PostAsJsonAsync(url, payload);
        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    [HttpPatch("profileDescription")]
    public async Task<IActionResult> ChangeProfileDescription([FromBody]string profileDescription)
    {
        AddJwtToHttpClientHeader();

        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var url = _config["MicroservicesUrl:User"] + $"/user/{userId}/profileDescription";  //todo: change to send currentUserId, profileUserId to allow mod to change someone's profile

        var response = await _httpClient.PatchAsJsonAsync(url, profileDescription);
        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}

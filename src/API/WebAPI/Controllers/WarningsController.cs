using Common.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebAPI.Helpers;
using WebAPI.Model.Warning;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarningsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public WarningsController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    private void AddJwtToHttpClientHeader()
    {
        var jwtBearer = HttpContext.Request.Headers.Authorization;
        if (jwtBearer.Count == 0)
        {
            return;
        }

        var jwt = jwtBearer.ToString().Replace("bearer ", "");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt); // todo: use middleware to set authorization header
    }

    // GET: api/<WarningsController>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<WarningDto>))]
    public async Task Get([FromQuery] bool onlyInactive = false,
            [FromQuery] bool onlyInactiveForUser = false)
    {
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings?onlyInactive={onlyInactive}&onlyInactiveForUser={onlyInactiveForUser}";

        var request = HttpMessageHelper.CreateProxyHttpRequest(HttpContext, new Uri(url));
        var response = await _httpClient.SendAsync(request);
        await HttpMessageHelper.CopyProxyHttpResponse(HttpContext, response);
        return;
    }

    // GET api/<WarningsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(WarningDto))]
    public async Task Get(Guid id)
    {
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings/{id}";

        var request = HttpMessageHelper.CreateProxyHttpRequest(HttpContext, new Uri(url));
        var response = await _httpClient.SendAsync(request);
        await HttpMessageHelper.CopyProxyHttpResponse(HttpContext, response);
        return;
    }

    // POST api/<WarningsController>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] AddWarningCommand request)    //todo: set id to be nullable in post and not in put
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings";

        var response = await _httpClient.PostAsJsonAsync(url, request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    // PUT api/<WarningController>/5
    //todo: [HttpPut("{id}")]
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody] UpdateWarningCommand request)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings";

        var response = await _httpClient.PutAsJsonAsync(url, request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpPatch("{id:guid}/changeStatus")]
    [Authorize(Roles = AuthUserRole.Moderator)]
    public async Task ChangeLocationStatus([FromRoute] Guid id, [FromQuery] bool changeToActive)
    {
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings/{id}/changeStatus?changeToActive={changeToActive}";

        var request = HttpMessageHelper.CreateProxyHttpRequest(HttpContext, new Uri(url));
        var response = await _httpClient.SendAsync(request);
        await HttpMessageHelper.CopyProxyHttpResponse(HttpContext, response);
        return;
    }

    // DELETE api/<WarningController>/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings/{id}";

        var response = await _httpClient.DeleteAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpPost("{id:guid}/reaction")]
    [Authorize]
    public async Task<IActionResult> PostReaction([FromRoute] Guid id, bool approve)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings/{id}/reaction?approve={approve}";

        var response = await _httpClient.PostAsync(url, null);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpGet("{id:guid}/userReaction/{userId:guid}")]
    public async Task<bool?> GetReactionForUser([FromRoute] Guid id, [FromRoute] Guid userId)   //todo: can return true, false or 204 for no reaction
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Warning"] + $"/warnings/{id}/userReaction/{userId}";

        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<bool?>();
            return result;
        }
        else if (response.StatusCode is System.Net.HttpStatusCode.NoContent or System.Net.HttpStatusCode.Unauthorized)  //todo: why unauthorized?
        {
            return null;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}

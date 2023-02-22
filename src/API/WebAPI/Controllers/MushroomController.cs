using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebAPI.Model.Mushroom;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MushroomController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public MushroomController(HttpClient httpClient, IConfiguration config)
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

    [HttpGet("{id:guid}")]
    public async Task<Mushroom> Get(Guid id)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Mushroom"] + $"/mushroom/{id}";

        var response = await _httpClient.GetAsync(url);     //todo: dispatcher
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<Mushroom>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpGet]
    public async Task<IEnumerable<Mushroom>> Get()
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Mushroom"] + $"/mushroom";

        var response = await _httpClient.GetAsync(url);     //todo: dispatcher
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<Mushroom>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MushroomDto request)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Mushroom"] + $"/mushroom";

        var response = await _httpClient.PostAsJsonAsync(url, request);     //todo: dispatcher
        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Mushroom"] + $"/mushroom/{id}";

        var response = await _httpClient.DeleteAsync(url);     //todo: dispatcher
        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] MushroomDto request)
    {
        AddJwtToHttpClientHeader();
        var url = _config["MicroservicesUrl:Mushroom"] + $"/mushroom/{id}";

        var response = await _httpClient.PatchAsJsonAsync(url, request);     //todo: dispatcher
        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else
        {
            throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
        }
    }
}

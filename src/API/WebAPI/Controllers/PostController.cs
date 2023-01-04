using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using WebAPI.Model.Post;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PostController(IMediator mediator, HttpClient httpClient, IConfiguration config)
        {
            _mediator = mediator;
            _httpClient = httpClient;
            _config = config;
        }

        private void AddJwtToHttpClientHeader()
        {
            var jwt = HttpContext.Request.Headers.Authorization;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt); // todo: use middleware to set authorization header
        }

        [HttpGet("{id:guid}")]
        public async Task<PostReadModel> Get([FromRoute] Guid id)
        {
            AddJwtToHttpClientHeader();
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}";

            var response = await _httpClient.GetAsync(url);     //todo: dispatcher
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PostReadModel>();
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase); //todo: error handling with proper message //todo return BadRequest();    
            }
        }

        [HttpGet]
        public async Task<IEnumerable<PostReadModel>> Get()
        {
            var jwt = HttpContext.Request.Headers.Authorization;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt); // todo: use middleware to set authorization header

            var response = await _httpClient.GetAsync("https://localhost:7174/api/Post");     //todo: dispatcher
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<PostReadModel>>();
                return result;
            }
            else
            {
                //return BadRequest();    
                throw new Exception(response.ReasonPhrase); //todo: error handling with proper message
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddPostCommand request)
        {
            AddJwtToHttpClientHeader();
            var url = _config["MicroservicesUrl:Post"] + $"/post";

            var response = await _httpClient.PostAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);  
            }
        }

        [HttpPost("{id:guid}/comment")]
        public async Task<IActionResult> PostComment([FromRoute] Guid id, [FromBody] string content)
        {
            AddJwtToHttpClientHeader();

            var authorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/comment?authorid={authorId}";

            var response = await _httpClient.PostAsJsonAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpGet("{id:guid}/comment")]
        public async Task<IEnumerable<CommentReadModel>> GetComments([FromRoute] Guid id)
        {
            AddJwtToHttpClientHeader();
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/comment";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<CommentReadModel>>();
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpPost("{id:guid}/reaction")]
        public async Task<IActionResult> PostReaction([FromRoute] Guid id, bool like)
        {
            AddJwtToHttpClientHeader();

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/reaction?like={like}&userid={userId}";

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, url));
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpGet("{id:guid}/reactionForUser")]
        public async Task<bool?> GetReactionForUser([FromRoute] Guid id)
        {
            AddJwtToHttpClientHeader();

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/reactionForUser?userId={userId}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool?>();
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}

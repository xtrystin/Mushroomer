using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using WebAPI.Model.Post;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public PostController(HttpClient httpClient, IConfiguration config)
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",jwt); // todo: use middleware to set authorization header
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
            var jwt = HttpContext.Request.Headers.Authorization;    //todo refactor
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
        public async Task<IActionResult> Post([FromBody] AddPostDto request)
        {
            AddJwtToHttpClientHeader();
            var url = _config["MicroservicesUrl:Post"] + $"/post";
            var authorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            AddPostCommand post = new() { Title = request.Title, Content= request.Content, AuthorId = authorId }; 
            
            var response = await _httpClient.PostAsJsonAsync(url, post);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);  
            }
        }

        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> EditPost([FromRoute] Guid id, EditPostCommandDto request)
        {
            AddJwtToHttpClientHeader();
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}?userId={userId}";

            var response = await _httpClient.PatchAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            AddJwtToHttpClientHeader();
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}?userId={userId}";

            var response = await _httpClient.DeleteAsync(url);
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

        [HttpGet("comment/user/{id:guid}")]
        public async Task<IEnumerable<CommentReadModel>> GetCommentsForUser([FromRoute] Guid id)
        {
            AddJwtToHttpClientHeader();
            var url = _config["MicroservicesUrl:Post"] + $"/post/comment/user/{id}";

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

        [HttpPatch("{id:guid}/comment/{commentId:guid}")]
        public async Task<IActionResult> ModifyComment([FromRoute] Guid id, [FromBody] string content, [FromRoute] Guid commentId)
        {
            AddJwtToHttpClientHeader();

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/comment/{commentId}?userId={userId}";

            var response = await _httpClient.PatchAsJsonAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpDelete("{id:guid}/comment/{commentId:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id, [FromRoute] Guid commentId)
        {
            AddJwtToHttpClientHeader();
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/comment/{commentId}?userId={userId}";

            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
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
        [Authorize]
        public async Task<bool?> GetReactionForUser([FromRoute] Guid id)
        {
            AddJwtToHttpClientHeader();

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var url = _config["MicroservicesUrl:Post"] + $"/post/{id}/reactionForUser?userId={userId}";

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = await response.Content?.ReadFromJsonAsync<bool?>();
                return result;
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}

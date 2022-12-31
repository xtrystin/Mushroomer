using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
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

        public PostController(IMediator mediator, HttpClient httpClient)
        {
            _mediator = mediator;
            _httpClient = httpClient;
        }

        [HttpGet("{id:guid}")]
        public async Task<PostReadModel> Get([FromRoute] Guid id)
        {
            var request = new GetPostQuery { Id = id };
            var result = await _mediator.Send(request);

            return result;
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
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("{id:guid}/comment")]
        public async Task<IActionResult> PostComment([FromRoute] Guid id, [FromBody] string content)
        {
            var request = new AddCommentCommand { PostId = id, Content = content };
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("{id:guid}/comment")]
        public async Task<IEnumerable<CommentReadModel>> GetComments([FromRoute] Guid id)
        {
            var request = new GetCommentsForPostQuery { PostId = id };
            var result = await _mediator.Send(request);

            return result;
        }
    }
}

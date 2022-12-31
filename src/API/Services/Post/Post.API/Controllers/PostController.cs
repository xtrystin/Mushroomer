using MediatR;
using Microsoft.AspNetCore.Mvc;
using Post.Application.Command;
using Post.Application.Dto;
using Post.Application.Query;
using System.Security.Claims;

namespace Post.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<PostReadModel> Get([FromRoute]Guid id)
        {
            var request = new GetPostQuery { Id = id };
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<PostReadModel>> Get()
        {
            var request = new GetAllPostsQuery();
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddPostCommand request)
        {
            //var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);   //todo
            await _mediator.Send(request);
            
            return Ok();
        }

        [HttpPost("{id:guid}/comment")]
        public async Task<IActionResult> PostComment([FromRoute]Guid id, [FromBody]string content, Guid authorId)
        {
            //var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);   //todo
            var request = new AddCommentCommand { PostId= id, Content = content, AuthorId = authorId };
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

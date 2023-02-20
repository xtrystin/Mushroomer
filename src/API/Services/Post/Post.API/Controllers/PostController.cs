﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Post.API.Dto;
using Post.Application.Command;
using Post.Application.Dto;
using Post.Application.Query;
using System.Net;
using System.Security.Claims;

namespace Post.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        // [ProducesResponseType(typeof(PostReadModel), 200)]
        //[ProducesResponseType(404)]
        public async Task<PostReadModel> Get([FromRoute]Guid id)
        {
            var request = new GetPostQuery { Id = id };
            var result = await _mediator.Send(request);

            return result;
           // return result is null ? Ok(result) : NotFound();  //todo
        }

        [HttpGet]
        [AllowAnonymous]
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

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> EditPost([FromRoute]Guid id, EditPostCommandDto requestDto, Guid userId)
        {
            var request = new EditPostCommand() { PostId = id, Title = requestDto.Title, 
                Content = requestDto.Content, UserId = userId};
            await _mediator.Send(request);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id, Guid userId)
        {
            var request = new DeletePostCommand() { PostId = id, UserId = userId };
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost("{id:guid}/comment")]
        public async Task<IActionResult> PostComment([FromRoute]Guid id, [FromBody]string content, Guid authorId)
        {
            //var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);   //todo
            var request = new AddCommentCommand { PostId = id, Content = content, AuthorId = authorId };
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("{id:guid}/comment")]
        [AllowAnonymous]
        public async Task<IEnumerable<CommentReadModel>> GetComments([FromRoute] Guid id)
        {
            var request = new GetCommentsForPostQuery { PostId = id };
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpGet("comment/user/{id:guid}")]
        [AllowAnonymous]
        public async Task<IEnumerable<CommentReadModel>> GetCommentsForUser([FromRoute] Guid id)
        {
            var request = new GetCommentsForUserQuery { UserId = id };
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPatch("{id:guid}/comment/{commentId:guid}")]
        public async Task ModifyComment([FromRoute] Guid id, [FromBody]string content, [FromRoute]Guid commentId, [FromQuery]Guid userId)
        {
            var request = new ModifyCommentCommand { PostId = id, CommentId = commentId, Content = content, UserId = userId };
            await _mediator.Send(request);
        }

        [HttpDelete("{id:guid}/comment/{commentId:guid}")]
        public async Task DeleteComment([FromRoute] Guid id, [FromRoute] Guid commentId, Guid userId)
        {
            var request = new DeleteCommentCommand { PostId = id, CommentId = commentId, UserId = userId };
            await _mediator.Send(request);
        }

        [HttpPost("{id:guid}/reaction")]
        public async Task<IActionResult> PostReaction([FromRoute] Guid id, bool like, Guid userId)
        {
            //var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);   //todo
            var request = new AddReactionToPostCommand { PostId = id, UserId = userId, Like = like };
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet("{id:guid}/reactionForUser")]
        [AllowAnonymous]
        public async Task<bool?> GetReactionForUser([FromRoute] Guid id, Guid userId)   //todo: can return true, false or 204 for no reaction
        {
            //var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);   //todo
            var request = new GetReactionQuery { PostId = id, UserId= userId };
            var result = await _mediator.Send(request);

            return result;
        }
    }
}

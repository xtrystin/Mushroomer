using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User.API.Dto;
using User.Application.Command;
using User.Application.Query;
using User.Application.ReadModel;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<UserReadModel>> Get()     //todo: do not return friends for getAll
        {
            var request = new GetAllUsersQuery();
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpGet("{id:guid}")]
        public async Task<UserReadModel> Get([FromRoute] Guid id)
        {
            //todo: use User.Identity.Name and add [Authorize]
            var request = new GetUserQuery { Id = id };
            var result = await _mediator.Send(request);

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserCommand request)
        {
            await _mediator.Send(request);  // todo: only authService can access this endpoint
            return Ok();
        }

        [HttpPost("Friend")]
        [Authorize]
        [EndpointDescription("Add or remove friend to/from friend list. Set add to true if you want to add. For removing set add to false")]
        public async Task<IActionResult> AddFriend([FromBody]ChangeFriendCommandDto requestDto, [FromQuery]bool add = true)
        {
            var actionAuthorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);     // todo: move to middleware: ActionContext?
            var actionAuthorRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var request = new ChangeFriendCommand() { UserId = requestDto.UserId, FriendId = requestDto.FriendId, Add = add,
                ActionAuthorId = actionAuthorId, ActionAuthorRole = actionAuthorRole };

            await _mediator.Send(request);
            return Ok();
        }

        [HttpPatch("{id:guid}/profileDescription")]
        [Authorize]
        public async Task<IActionResult> ChangeProfileDescriptuon([FromRoute]Guid id, [FromBody]string profileDescription)
        {
            var actionAuthorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);     // todo: move to middleware: ActionContext?
            var actionAuthorRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var request = new ChangeProfileDescriptionCommand() { UserId = id, ProfileDescription = profileDescription,
                ActionAuthorId = actionAuthorId, ActionAuthorRole = actionAuthorRole};

            await _mediator.Send(request);
            return Ok();
        }
    }
}

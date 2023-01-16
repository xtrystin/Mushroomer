using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost("Friend")]
        [EndpointDescription("Add or remove friend to/from friend list. Set add to true if you want to add. For removing set add to false")]
        public async Task<IActionResult> AddFriend([FromBody]ChangeFriendCommandDto requestDto, [FromQuery]bool add = true)
        {
            var request = new ChangeFriendCommand() { UserId = requestDto.UserId, 
                FriendId = requestDto.FriendId, Add = add };

            await _mediator.Send(request);
            return Ok();
        }

        [HttpPatch("{id:guid}/profileDescription")]
        public async Task<IActionResult> ChangeProfileDescriptuon([FromRoute]Guid id, [FromBody]string profileDescription)
        {
            var request = new ChangeProfileDescriptionCommand() { UserId = id, ProfileDescription = profileDescription };
            await _mediator.Send(request);

            return Ok();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    }
}

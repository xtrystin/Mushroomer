using Application.Commands;
using Application.Dto;
using Application.Queries;
using Common.Const;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Warning.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarningsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarningsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<WarningsController>
    [HttpGet]
    public async Task<IEnumerable<WarningDto>> Get()
    {
        var request = new GetAllWarningsQuery();

        var result = await _mediator.Send(request);

        return result;
    }

    // GET api/<WarningsController>/5
    [HttpGet("{id}")]
    public async Task<WarningDto> Get(Guid id)
    {
        var request = new GetWarningQuery { Id = id };

        var result = await _mediator.Send(request);

        return result;
    }

    // POST api/<WarningsController>
    [HttpPost]
    [Authorize(Roles = $"{AuthUserRole.Moderator}, {AuthUserRole.Experienced}")]
    public async Task<IActionResult> Post([FromBody] AddWarningCommand request)    //todo: set id to be nullable in post and not in put
    {
        await _mediator.Send(request);

        return Ok();
    }

    // PUT api/<WarningController>/5
    //todo: [HttpPut("{id}")]
    [HttpPut]
    [Authorize(Roles = $"{AuthUserRole.Moderator}, {AuthUserRole.Experienced}")]
    public async Task<IActionResult> Put([FromBody] UpdateWarningCommand request)
    {
        await _mediator.Send(request);

        return Ok();
    }

    // DELETE api/<WarningController>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{AuthUserRole.Moderator}, {AuthUserRole.Experienced}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteWarningCommand { Id = id };

        var result = await _mediator.Send(request);

        return Ok();
    }

    [HttpPost("{id:guid}/reaction")]
    [Authorize]
    public async Task<IActionResult> PostReaction([FromRoute] Guid id, bool approve)
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var request = new AddReactionToWarningCommand { WarningId = id, UserId = userId, Approve = approve };
        await _mediator.Send(request);

        return Ok();
    }

    [HttpGet("{id:guid}/userReaction")]
    public async Task<bool?> GetReactionForUser([FromRoute] Guid id)   //todo: can return true, false or 204 for no reaction
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var request = new GetReactionQuery { WarningId = id, UserId = userId };
        var result = await _mediator.Send(request);

        return result;
    }
}

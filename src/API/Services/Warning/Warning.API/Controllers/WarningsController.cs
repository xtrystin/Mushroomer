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

    [HttpGet]
    public async Task<IEnumerable<WarningDto>> Get([FromQuery] bool onlyInactive = false, 
        [FromQuery] bool onlyInactiveForUser = false)
    {
        bool isUserMod = User?.IsInRole(AuthUserRole.Moderator) ?? false;
        string? userEmail = (User?.Identity.IsAuthenticated).GetValueOrDefault() ? User.FindFirst(ClaimTypes.Name).Value : null;
        var request = new GetAllWarningsQuery
        {
            OnlyInactive = onlyInactive,
            IsUserMod = isUserMod,
            OnlyInactiveForUser = onlyInactiveForUser,
            UserEmail = userEmail
        };

        var result = await _mediator.Send(request);

        return result;
    }

    [HttpGet("{id}")]
    public async Task<WarningDto> Get(Guid id)
    {
        bool isUserMod = User?.IsInRole(AuthUserRole.Moderator) ?? false;
        Guid? userId = User.Identity.IsAuthenticated ? new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value) : null;

        var request = new GetWarningQuery { Id = id, UserId = userId, IsUserMod = isUserMod };

        var result = await _mediator.Send(request);

        return result;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] AddWarningCommand request)    //todo: set id to be nullable in post and not in put
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        request.AuthorId = userId;
        request.AutoActivate = User.IsInRole(AuthUserRole.Moderator);

        // everyone can access
        await _mediator.Send(request);

        return Ok();
    }

    //todo: [HttpPut("{id}")]
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Put([FromBody] UpdateWarningCommand request)
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        bool isUserMod = User?.IsInRole(AuthUserRole.Moderator) ?? false;
        request.UserId = userId;
        request.IsUserMod = isUserMod;

        // cannot change isActive
        await _mediator.Send(request);

        return Ok();
    }

    [HttpPatch("{id:guid}/changeStatus")]
    [Authorize(Roles = AuthUserRole.Moderator)]
    public async Task<IActionResult> ChangePostStatus([FromRoute] Guid id, [FromQuery] bool changeToActive)
    {
        var request = new ChangeLocationStatusCommand { LocationId = id, ChangeToActive = changeToActive };
        await _mediator.Send(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        bool isUserMod = User?.IsInRole(AuthUserRole.Moderator) ?? false;
        var request = new DeleteWarningCommand { Id = id, UserId = userId, IsUserMod = isUserMod };

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

    [HttpGet("{id:guid}/userReaction/{userId:guid}")]
    public async Task<bool?> GetReactionForUser([FromRoute] Guid id, [FromRoute] Guid userId)   //todo: can return true, false or 204 for no reaction
    {
        //var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var request = new GetReactionQuery { WarningId = id, UserId = userId };
        var result = await _mediator.Send(request);

        return result;
    }
}

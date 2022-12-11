using Application.Dto;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

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
    public IActionResult Post([FromBody] string value)
    {
        return BadRequest();
    }

    // PUT api/<WarningController>/5
    [HttpPut("{id}")]
    public void Put(Guid id, [FromBody] string value)
    {
    }

    // DELETE api/<WarningController>/5
    [HttpDelete("{id}")]
    public void Delete(Guid id)
    {
    }
}

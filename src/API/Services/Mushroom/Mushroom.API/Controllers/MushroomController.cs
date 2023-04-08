using Common.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mushroom.API.Model;
using System.Data;
using WebApplication1.EF;

namespace Mushroom.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MushroomController : ControllerBase
{
    private readonly MushroomDbContext _dbContext;

    public MushroomController(MushroomDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Model.Mushroom>> Get()
    {
        return await _dbContext.Mushrooms.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<Model.Mushroom> Get([FromRoute]Guid id)
    {
        return await _dbContext.Mushrooms.FirstOrDefaultAsync(x => x.Id == id);
    }

    [HttpPost]
    [Authorize(Roles = AuthUserRole.Moderator)]
    public async Task<IActionResult> Create([FromBody] MushroomDto request)
    {
        Model.Mushroom newMushroom = new()
        {
            Id = default,
            Name = request.Name,
            Description = request.Description,
            CreatedDate = DateTime.Now,
            IsPoisonous = request.IsPoisonous,
            PhotoUrl = request.PhotoUrl
        };

        await _dbContext.AddAsync(newMushroom);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = AuthUserRole.Moderator)]
    public async Task Delete([FromRoute]Guid id)
    {
        var mushroom = await _dbContext.Mushrooms.FirstOrDefaultAsync(_ => _.Id == id);
        
        _dbContext.Mushrooms.Remove(mushroom);
        await _dbContext.SaveChangesAsync();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = AuthUserRole.Moderator)]
    public async Task Update([FromRoute]Guid id, [FromBody] MushroomDto request)
    {
        var mushroom = await _dbContext.Mushrooms.FirstOrDefaultAsync(_ => _.Id == id);

        mushroom.Name = request.Name;
        mushroom.Description = request.Description;
        mushroom.IsPoisonous = request.IsPoisonous;
        mushroom.PhotoUrl= request.PhotoUrl;

        _dbContext.Update(mushroom);
        await _dbContext.SaveChangesAsync();
    }
}

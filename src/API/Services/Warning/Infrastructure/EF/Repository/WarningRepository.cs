using Domain;
using Domain.Entity;
using Domain.Repository;
using Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository;

public class WarningRepository : IWarningRepository
{
    private readonly WarningDbContext _dbContext;
    private readonly DbSet<Warning> _warnings;

    public WarningRepository(WarningDbContext dbContext)
	{
        _dbContext = dbContext;
        _warnings = dbContext.Warnings;
    }

    public async Task AddWarningAsync(Warning warning)
    {
        await _warnings.AddAsync(warning);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteWarningAsync(Warning warning)
    {
        _warnings.Remove(warning);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Warning>> GetAllWarningsAsync()
    {
        return await _warnings.Include(x => x._reactions).ToListAsync();
    }

    public async Task<Warning> GetWarningAsync(Guid id)
    {
        return await _warnings.Include(x => x._reactions).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateWarningAsync(Warning warning)
    {
        _warnings.Update(warning);
        await _dbContext.SaveChangesAsync();
    }
}

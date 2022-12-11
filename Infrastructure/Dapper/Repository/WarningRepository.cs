using Domain;
using Domain.Repository;

namespace Infrastructure.Dapper.Repository;

public class WarningRepository : IWarningRepository
{
    public Task AddWarningAsync(Warning warning)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWarningAsync(Warning warning)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Warning>> GetAllWarningsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Warning> GetWarningAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateWarningAsync(Warning warning)
    {
        throw new NotImplementedException();
    }
}

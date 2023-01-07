using Domain.Entity;

namespace Domain.Repository;

public interface IWarningRepository
{
    Task<Warning> GetWarningAsync(Guid id);
    Task<IEnumerable<Warning>> GetAllWarningsAsync();
    Task AddWarningAsync(Warning warning);
    Task UpdateWarningAsync(Warning warning);
    Task DeleteWarningAsync(Warning warning);
}

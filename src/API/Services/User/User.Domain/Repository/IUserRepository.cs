using User.Domain.ValueObject;

namespace User.Domain.Repository;

public interface IUserRepository
{
    Task<Entity.User> GetAsync(UserId id);
    Task AddAsync(Entity.User user);
    Task UpdateAsync(Entity.User user);
    Task DeleteAsync(Entity.User user);
}

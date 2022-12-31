namespace Post.Domain.Repository;

public interface IUserRepository
{
    Task<Entity.User> GetAsync(Guid id);
}

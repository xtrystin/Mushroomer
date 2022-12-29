using Post.Domain.ValueObject;

namespace Post.Domain.Repository;

public interface IPostRepository
{
    Task<Entity.Post> GetAsync(PostId id);
    Task AddAsync(Entity.Post post);
    Task UpdateAsync(Entity.Post post);
    Task DeleteAsync(Entity.Post post);
}

using Microsoft.EntityFrameworkCore;
using Post.Domain.Repository;
using Post.Domain.ValueObject;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF.Repository;

public class PostRepository : IPostRepository
{
    private readonly PostDbContext _dbContext;
    private readonly DbSet<Domain.Entity.Post> _posts;

    public PostRepository(PostDbContext dbContext)
    {
        _dbContext = dbContext;
        _posts = dbContext.Posts;
    }

    public async Task AddAsync(Domain.Entity.Post post)
    {
        await _posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Domain.Entity.Post post)
    {
        _posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entity.Post> GetAsync(PostId id)
    {
        return await _posts.Include("_comments").Include("_reactions").FirstOrDefaultAsync(post => post.Id == id);
    }

    public async Task UpdateAsync(Domain.Entity.Post post)
    {
        _dbContext.Posts.Update(post); await _dbContext.SaveChangesAsync();
        //try
        //{
        //    int updates = await _dbContext.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException ex)
        //{
        //    foreach (var entry in ex.Entries)
        //    {
        //        var proposedValues = entry.CurrentValues;
        //        var databaseValues = entry.GetDatabaseValues();
        //        var original = entry.OriginalValues;

        //        foreach (var property in proposedValues.Properties)
        //        {
        //            var orig = original[property];
        //            var proposedValue = proposedValues[property];
        //            var databaseValue = databaseValues[property];
        //        }
        //    }
        //    throw;
        //}
        //catch(Exception ex) 
        //{
        //    throw;
        //}
    }
}

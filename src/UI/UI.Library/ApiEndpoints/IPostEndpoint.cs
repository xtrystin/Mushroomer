using UI.ApiLibrary.Dto.Post;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IPostEndpoint
{
    Task<PostReadModel> Get(Guid id);
    Task<IEnumerable<PostReadModel>> GetAll();
    Task Add(AddPostDto post);
    Task Edit(Guid id, EditPostCommandDto editPostDto);
    Task Delete(Guid id);
    Task<IEnumerable<CommentReadModel>> GetCommentsForPost(Guid postId);
    Task AddCommentToPost(AddCommentDto comment);
    Task<bool?> GetReactionForUser(Guid postId);
    public Task PostReaction(Guid postId, bool like);
}

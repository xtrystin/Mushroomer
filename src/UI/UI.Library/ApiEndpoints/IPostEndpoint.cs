using UI.ApiLibrary.Dto.Post;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IPostEndpoint
{
    Task<PostReadModel> Get(Guid id);
    Task<IEnumerable<PostReadModel>> GetAll(bool onlyInactive = false, bool onlyInactiveForUser = false);
    Task Add(AddPostDto post);
    Task Edit(Guid id, EditPostCommandDto editPostDto);
    Task Delete(Guid id);
    Task ChangeStatus(Guid id, bool changeToActive);
    Task<IEnumerable<CommentReadModel>> GetCommentsForPost(Guid postId);
    Task<IEnumerable<CommentReadModel>> GetCommentsForUser(Guid userId);
    Task ModifyComment(Guid postId, string content, Guid commentId);
    Task DeleteComment(Guid postId, Guid commentId);
    Task AddCommentToPost(AddCommentDto comment);
    Task<bool?> GetReactionForUser(Guid postId);
    public Task PostReaction(Guid postId, bool like);
}

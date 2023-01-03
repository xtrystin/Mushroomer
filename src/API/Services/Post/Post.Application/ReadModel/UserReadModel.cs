using Post.Application.Dto;
using Post.Domain.Entity;

namespace Post.Application.ReadModel;

public class UserReadModel      // todo: refactor
{
    public Guid Id { get; private set; }

    private string _firstName;
    private string _lastName;
    public string Email { get; set; }
    private readonly List<PostReadModel> _posts;
    private readonly List<CommentReadModel> _comments;
    private readonly List<PostUserReactionReadModel> _postReactions;
}

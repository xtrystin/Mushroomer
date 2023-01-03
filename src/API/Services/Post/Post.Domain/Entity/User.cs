using Post.Domain.ValueObject;

namespace Post.Domain.Entity;

public class User
{
    public Guid Id { get; private set; }

    private string _firstName;
    private string _lastName;
    private string _email;
    private readonly List<Post> _posts;
    private readonly List<Comment> _comments;
    private List<PostUserReaction> _postReactions;

    public User(Guid id, string firstName, string lastName, string email, List<Post> posts)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _posts = posts;
    }

    public User()
    {
    }
}

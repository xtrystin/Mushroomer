namespace Post.Domain.Exception;

public class UserAlreadyReactedToPost : System.Exception
{
    public UserAlreadyReactedToPost(string? message) : base(message)
    {
    }
}

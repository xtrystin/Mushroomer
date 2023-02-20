namespace Post.Application.Exception;

public class NotAuthorizedToDeletePost : System.Exception
{
    public NotAuthorizedToDeletePost() : base("You are not authorized to delete this post.")
    {
    }
}

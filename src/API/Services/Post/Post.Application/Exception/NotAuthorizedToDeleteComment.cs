namespace Post.Application.Exception;

public class NotAuthorizedToDeleteComment : System.Exception
{
    public NotAuthorizedToDeleteComment() : base("You are not authorized to delete this comment.")
    {
    }
}

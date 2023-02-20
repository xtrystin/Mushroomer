namespace Post.Application.Exception;

public class NotAuthorizedToModifyComment : System.Exception
{
    public NotAuthorizedToModifyComment() : base("You are not authorized to modify this comment.")
    {
    }
}

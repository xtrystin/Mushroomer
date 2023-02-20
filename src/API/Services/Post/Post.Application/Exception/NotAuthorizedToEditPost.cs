namespace Post.Application.Exception;

public class NotAuthorizedToEditPost : System.Exception
{
	public NotAuthorizedToEditPost() : base("You are not authorized to edit this post.")
	{

	}
}

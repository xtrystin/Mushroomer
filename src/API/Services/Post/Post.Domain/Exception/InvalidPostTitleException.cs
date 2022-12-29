namespace Post.Domain.Exception;

public class InvalidPostTitleException : System.Exception
{
	public InvalidPostTitleException() : base("Post Title cannot be empty nor longer than 20 characters")
	{

	}
}

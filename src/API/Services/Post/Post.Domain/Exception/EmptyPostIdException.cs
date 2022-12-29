namespace Post.Domain.Exception;

public class EmptyPostIdException : System.Exception
{
	public EmptyPostIdException() : base("PostId cannot be empty")
	{
	}
}

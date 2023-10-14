using Common.Exception;

namespace Post.Domain.Exception;

public class InvalidPostTitleException : ApiException
{
	public InvalidPostTitleException() : base(System.Net.HttpStatusCode.BadRequest, "Post Title cannot be empty nor longer than 20 characters")
	{

	}
}

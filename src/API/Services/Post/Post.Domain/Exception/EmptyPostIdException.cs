using Common.Exception;

namespace Post.Domain.Exception;

public class EmptyPostIdException : ApiException
{
	public EmptyPostIdException() : base(System.Net.HttpStatusCode.BadRequest, "PostId cannot be empty")
	{
	}
}

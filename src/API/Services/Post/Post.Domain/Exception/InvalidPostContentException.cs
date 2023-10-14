using Common.Exception;

namespace Post.Domain.Exception;

public class InvalidPostContentException : ApiException
{
    public InvalidPostContentException() : base(System.Net.HttpStatusCode.BadRequest, "Post Content cannot be empty nor longer than 10000 characters")
    {

    }
}

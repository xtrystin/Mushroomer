using Common.Exception;

namespace Post.Domain.Exception;

public class UserAlreadyReactedToPost : ApiException
{
    public UserAlreadyReactedToPost(string? message) : base(System.Net.HttpStatusCode.BadRequest, message)
    {
    }
}

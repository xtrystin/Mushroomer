using Common.Exception;

namespace Post.Application.Exception;

public class NotAuthorizedToDeletePost : ApiException
{
    public NotAuthorizedToDeletePost() : base(System.Net.HttpStatusCode.Forbidden, "You are not authorized to delete this post.")
    {
    }
}

using Common.Exception;

namespace Post.Application.Exception;

public class NotAuthorizedToDeleteComment : ApiException
{
    public NotAuthorizedToDeleteComment() : base(System.Net.HttpStatusCode.Forbidden, "You are not authorized to delete this comment.")
    {
    }
}

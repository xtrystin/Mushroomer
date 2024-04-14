using Common.Exception;

namespace Post.Application.Exception;

public class NotAuthorizedToModifyComment : ApiException
{
    public NotAuthorizedToModifyComment() : base(System.Net.HttpStatusCode.Forbidden, "You are not authorized to modify this comment.")
    {
    }
}

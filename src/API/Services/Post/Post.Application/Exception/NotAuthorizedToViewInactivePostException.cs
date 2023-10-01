using Common.Exception;
using System.Net;

namespace Post.Application.Exception;
public class NotAuthorizedToViewInactivePostException : ApiException
{
    public NotAuthorizedToViewInactivePostException(string message) 
        : base(HttpStatusCode.Forbidden, message)
    {
    }
}

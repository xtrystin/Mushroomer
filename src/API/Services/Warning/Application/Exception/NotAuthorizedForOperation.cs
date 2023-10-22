using Common.Exception;
using System.Net;

namespace Application.Exception;
public class NotAuthorizedForOperation : ApiException
{
    public NotAuthorizedForOperation(string message) : base(HttpStatusCode.Forbidden, message)
    {
    }
}

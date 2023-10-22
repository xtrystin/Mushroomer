using Common.Exception;
using System.Net;

namespace Domain.Exception;

public class UserAlreadyReactedToWarning : ApiException
{
    public UserAlreadyReactedToWarning(string? message) : base(HttpStatusCode.BadRequest, message)
    {
    }
}

using Common.Exception;
using System.Net;

namespace Post.Application.Exception;
public class UserNotFoundException : ApiException
{
    public UserNotFoundException() : base(HttpStatusCode.NotFound, "User was not found")
    {
    }
}

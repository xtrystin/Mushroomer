using Common.Exception;
using System.Net;

namespace Application.Exception;

public class ItemNotFoundException : ApiException
{
    public ItemNotFoundException(string message) : base(HttpStatusCode.NotFound, message)
    {
    }
}

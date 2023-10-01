using Common.Exception;
using System.Net;

namespace Post.Application.Exception;

public class PostNotFoundException : ApiException
{
    public PostNotFoundException() : base(HttpStatusCode.NotFound, "Post was not found")
    {
    }
}

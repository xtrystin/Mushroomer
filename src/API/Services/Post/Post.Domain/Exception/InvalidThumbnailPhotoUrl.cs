using Common.Exception;
using System.Net;

namespace Post.Domain.Exception;
public class InvalidThumbnailPhotoUrl : ApiException
{
    public InvalidThumbnailPhotoUrl()
        : base(HttpStatusCode.BadRequest, "Post thumbnail photo url is not valid")
    {
    }
}

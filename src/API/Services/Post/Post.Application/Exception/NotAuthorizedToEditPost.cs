using Common.Exception;

namespace Post.Application.Exception;

public class NotAuthorizedToEditPost : ApiException
{
	public NotAuthorizedToEditPost() : base(System.Net.HttpStatusCode.Forbidden, "You are not authorized to edit this post.")
	{

	}
}

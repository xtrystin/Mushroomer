using System.Net;

namespace Common.Exception;
public class ApiException : System.Exception
{
    public HttpStatusCode StatusCode { get; }

    public ApiException(HttpStatusCode statusCode, string message) : base(message)
	{
        StatusCode = statusCode;
    }
    
}

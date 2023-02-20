namespace Post.Infrastructure.Services;

public class AuthServiceException : System.Exception
{
    public AuthServiceException() : base("Error occurred while sending request to AuthService")
    {
    }
}

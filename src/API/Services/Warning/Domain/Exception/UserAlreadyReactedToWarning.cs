namespace Domain.Exception;

public class UserAlreadyReactedToWarning : System.Exception
{
    public UserAlreadyReactedToWarning(string? message) : base(message)
    {
    }
}

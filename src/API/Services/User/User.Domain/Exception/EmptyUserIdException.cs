namespace User.Domain.Exception;

public class EmptyUserIdException : System.Exception
{
    public EmptyUserIdException() : base("UserId cannot be empty")
    {
    }
}

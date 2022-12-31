namespace User.Domain.Exception;

public class InvalidEmailAddressException : System.Exception
{
    public InvalidEmailAddressException() : base("Email address must contain @ character, cannot be longer than 100 characters nor be empty")
    {

    }
}

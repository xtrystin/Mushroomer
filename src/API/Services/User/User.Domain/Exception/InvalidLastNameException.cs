namespace User.Domain.Exception;

public class InvalidLastNameException : System.Exception
{
    public InvalidLastNameException() : base("LastName cannot be longer than 100 characters nor be empty")
    {

    }
}

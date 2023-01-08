namespace User.Domain.Exception;

public class TooLongProfileDescriptionException : System.Exception
{
    public TooLongProfileDescriptionException() : base("Profile description cannot be longer than 1000 characters")
    {

    }
}

namespace User.Domain.Exception;

public class InvalidPhotoUrl : System.Exception
{
    public InvalidPhotoUrl() : base("PhotoUrl cannot be longer than 200 characters nor be empty")
    {

    }
}

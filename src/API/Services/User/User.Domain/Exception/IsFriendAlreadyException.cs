namespace User.Domain.Exception;

public class IsFriendAlreadyException : System.Exception
{
    public IsFriendAlreadyException() : base("You are friends already")
    {
    }
}



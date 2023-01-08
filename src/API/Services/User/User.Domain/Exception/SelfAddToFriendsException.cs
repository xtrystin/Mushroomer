namespace User.Domain.Exception;

public class SelfAddToFriendsException : System.Exception
{
    public SelfAddToFriendsException() : base("You cannot add yourself to friends")
    {
    }
}

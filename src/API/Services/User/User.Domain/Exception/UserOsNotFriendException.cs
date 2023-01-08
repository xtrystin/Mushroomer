namespace User.Domain.Exception;

public class UserOsNotFriendException : System.Exception
{
	public UserOsNotFriendException() : base("You are not friends")
	{

	}
}

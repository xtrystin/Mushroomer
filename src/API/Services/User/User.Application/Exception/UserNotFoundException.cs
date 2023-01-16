namespace User.Application.Exception;

public class UserNotFoundException : System.Exception
{
	public UserNotFoundException() : base("User was not found")
	{

	}
}

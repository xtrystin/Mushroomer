namespace User.Domain.Exception;

public class InvalidFirstNameException : System.Exception
{
	public InvalidFirstNameException() : base("FirstName cannot be longer than 100 characters nor be empty")
	{

	}
}

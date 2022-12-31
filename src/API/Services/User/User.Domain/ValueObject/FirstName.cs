using User.Domain.Exception;

namespace User.Domain.ValueObject;

public record FirstName
{
    public string Value { get; }

	public FirstName(string value)
	{
		if (value.Count() > 100 || string.IsNullOrEmpty(value))
		{
			throw new InvalidFirstNameException();
		}

        Value = value;
    }

	public static implicit operator string(FirstName firstName)
		=> firstName.Value;

	public static implicit operator FirstName(string firstName)
		=> new FirstName(firstName);
}

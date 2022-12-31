using User.Domain.Exception;

namespace User.Domain.ValueObject;

public record LastName
{
    public string Value { get; }

    public LastName(string value)
    {
        if (value.Count() > 100 || string.IsNullOrEmpty(value))
        {
            throw new InvalidLastNameException();
        }

        Value = value;
    }

    public static implicit operator string(LastName lastName)
        => lastName.Value;

    public static implicit operator LastName(string lastName)
        => new LastName(lastName);
}

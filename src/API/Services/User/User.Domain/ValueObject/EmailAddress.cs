using User.Domain.Exception;

namespace User.Domain.ValueObject;

public record EmailAddress
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (value.Contains('@') is false || value.Count() > 100 || string.IsNullOrEmpty(value))
        {
            throw new InvalidEmailAddressException();
        }

        Value = value;
    }

    public static implicit operator string(EmailAddress email)
        => email.Value;

    public static implicit operator EmailAddress(string email)
        => new EmailAddress(email);
}

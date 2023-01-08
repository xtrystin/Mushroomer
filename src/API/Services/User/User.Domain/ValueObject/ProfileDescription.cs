using User.Domain.Exception;

namespace User.Domain.ValueObject;

public record ProfileDescription
{
    public string Value { get; }

    public ProfileDescription(string value)
    {
        if (value.Count() > 1000)
        {
            throw new TooLongProfileDescriptionException();
        }

        Value = value;
    }

    public static implicit operator string(ProfileDescription description)
        => description.Value;

    public static implicit operator ProfileDescription(string description)
        => new ProfileDescription(description);
}

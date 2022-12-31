using User.Domain.Exception;

namespace User.Domain.ValueObject;

public record UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyUserIdException();
        }

        Value = value;
    }

    public UserId()
    {

    }

    public static implicit operator Guid(UserId id)
        => id.Value;

    public static implicit operator UserId(Guid id)
        => new UserId(id);
}

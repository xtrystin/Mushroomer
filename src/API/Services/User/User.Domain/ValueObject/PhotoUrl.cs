using User.Domain.Exception;

namespace User.Domain.ValueObject;

public record PhotoUrl
{
    public string Value { get; }

    public PhotoUrl(string value)
    {
        if (value.Count() > 200)
        {
            throw new InvalidPhotoUrl();
        }

        Value = value;
    }

    public static implicit operator string(PhotoUrl photoUrl)
        => photoUrl.Value;

    public static implicit operator PhotoUrl(string photoUrl)
        => new PhotoUrl(photoUrl);
}

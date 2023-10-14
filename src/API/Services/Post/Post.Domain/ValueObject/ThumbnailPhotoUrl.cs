using Post.Domain.Exception;

namespace Post.Domain.ValueObject;
public class ThumbnailPhotoUrl
{
    public string? Value { get; }
    public static readonly int MaxLength = 400;

    public ThumbnailPhotoUrl(string? value)
    {
        if (string.IsNullOrEmpty(value) is false && (value.Count() > MaxLength
            || value.StartsWith("http") is false))
        {
            throw new InvalidThumbnailPhotoUrl();
        }

        Value = value;
    }

    public static implicit operator string(ThumbnailPhotoUrl postTitle)
        => postTitle.Value;

    public static implicit operator ThumbnailPhotoUrl(string content)
        => new(content);
}

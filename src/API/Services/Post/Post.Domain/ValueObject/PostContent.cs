using Post.Domain.Exception;

namespace Post.Domain.ValueObject;

public record PostContent
{
    public string Value { get; }

    public PostContent(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Count() > 10000)
        {
            throw new InvalidPostContentException();
        }

        Value = value;
    }

    public static implicit operator string(PostContent postTitle)
        => postTitle.Value;

    public static implicit operator PostContent(string content)
        => new(content);
}

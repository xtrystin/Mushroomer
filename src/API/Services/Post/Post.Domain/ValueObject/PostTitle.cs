using Post.Domain.Exception;

namespace Post.Domain.ValueObject;

public record PostTitle
{
    public string Value { get; }

	public PostTitle(string value)
	{
		if (string.IsNullOrEmpty(value) || value.Count() > 200)
		{
			throw new InvalidPostTitleException();
		}

		Value = value;
	}

	public static implicit operator string(PostTitle postTitle)
		=> postTitle.Value;

	public static implicit operator PostTitle(string title)
		=> new(title);
}

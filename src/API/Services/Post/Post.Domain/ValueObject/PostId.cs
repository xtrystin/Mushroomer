using Post.Domain.Exception;

namespace Post.Domain.ValueObject
{
    public record PostId
    {
        public Guid Value { get; }

        public PostId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyPostIdException();
            }

            Value = value;
        }

        public PostId()
        {

        }

        public static implicit operator Guid(PostId id)
            => id.Value;

        public static implicit operator PostId(Guid id)
            => new PostId(id);
    }
}

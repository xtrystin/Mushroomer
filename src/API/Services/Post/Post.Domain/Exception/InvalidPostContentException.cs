namespace Post.Domain.Exception;

public class InvalidPostContentException : System.Exception
{
    public InvalidPostContentException() : base("Post Content cannot be empty nor longer than 10000 characters")
    {

    }
}

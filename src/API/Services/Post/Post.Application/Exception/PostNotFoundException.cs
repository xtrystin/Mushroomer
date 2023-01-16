namespace Post.Application.Exception;

public class PostNotFoundException : System.Exception
{
    public PostNotFoundException() : base("Post was not found")
    {

    }
}

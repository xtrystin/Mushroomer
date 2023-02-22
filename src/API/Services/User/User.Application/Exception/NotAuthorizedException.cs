namespace User.Application.Exception
{
    public class NotAuthorizedException : System.Exception
    {
        public NotAuthorizedException(string message) : base(message)
        {

        }
    }
}

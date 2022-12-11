namespace WebAssemblyUI.Contracts
{
    public class UserCredentials
    {
        public UserCredentials(string userName, string password, string grant_type)
        {
            UserName = userName;
            Password = password;
            Grant_type = grant_type;
        }

        public string UserName { get; init; }
        public string Password { get; init; }
        public string Grant_type { get; init; }

        
    }
}

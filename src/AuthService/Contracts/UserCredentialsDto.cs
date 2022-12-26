namespace AuthService.Contracts
{
    public class UserCredentialsDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Grant_type { get; set; }
    }
}

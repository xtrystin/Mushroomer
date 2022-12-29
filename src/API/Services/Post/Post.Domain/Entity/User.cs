namespace Post.Domain.Entity;

public class User
{
    public Guid Id { get; private set; }

    private string _firstName;
    private string _lastName;
    private string _email;
}

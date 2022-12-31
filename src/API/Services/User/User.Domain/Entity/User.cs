using User.Domain.ValueObject;

namespace User.Domain.Entity;

public class User
{
    public UserId Id { get; private set; }

    private FirstName _firstName;
    private LastName _lastName;
    private EmailAddress _emailAddress;

    public User(UserId id, FirstName firstName, LastName lastName, EmailAddress emailAddress)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _emailAddress = emailAddress;
    }

    public User()
    {
    }

    public void ChangeFirstName(FirstName firstName) => _firstName = firstName;
}

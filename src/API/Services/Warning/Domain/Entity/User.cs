namespace Domain.Entity;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    private List<WarningUserReaction> _warningReactions;
    private List<Warning> _warnings;

    public User(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public User()
    {
    }
}

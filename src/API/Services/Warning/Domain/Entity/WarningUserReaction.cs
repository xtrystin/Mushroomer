namespace Domain.Entity;

public class WarningUserReaction
{
    public Guid Id { get; set; }
    public bool Approve { get; set; } // todo: enum to have more reactions: happy, sad, like, dislikehttps://stackoverflow.com/questions/1434298/sql-server-equivalent-to-mysql-enum-data-type

    public User User { get; set; }
    public Guid UserId { get; set; }
    public Warning Warning { get; set; }
    public Guid WarningId { get; set; }
}

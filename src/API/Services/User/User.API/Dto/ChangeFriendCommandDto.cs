namespace User.API.Dto;

public class ChangeFriendCommandDto
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
}

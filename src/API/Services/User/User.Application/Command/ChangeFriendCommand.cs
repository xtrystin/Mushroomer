using MediatR;

namespace User.Application.Command;

public class ChangeFriendCommand : IRequest     //todo rename to changeFriendStatus
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public bool Add { get; set; }
}

using Common.Const;
using MediatR;
using User.Application.Exception;
using User.Domain.Repository;

namespace User.Application.Command.Handler;

public class ChangeFriendCommandHandler : IRequestHandler<ChangeFriendCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeFriendCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangeFriendCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != request.ActionAuthorId && request.ActionAuthorRole != AuthUserRole.Moderator)
        {
            throw new NotAuthorizedException("You are not authorized to add user to friend list");
        }

        var user = await _userRepository.GetAsync(request.UserId);      
        var friend = await _userRepository.GetAsync(request.FriendId);

        if (request.Add)
        {
            user.AddFriend(friend);
        }
        else
        {
            user.RemoveFriend(friend);

            ///todo 223: this is hack for problems with deleting row in many to self many relation, EF wants to set null to foreign key column isntead of deleting the whole row
            friend.RemoveFriendToUsers(user);  
            await _userRepository.UpdateAsync(friend);
        }

        await _userRepository.UpdateAsync(user);
        return Unit.Value;
    }
}

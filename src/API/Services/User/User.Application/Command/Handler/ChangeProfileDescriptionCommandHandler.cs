﻿using Common.Const;
using MediatR;
using User.Application.Exception;
using User.Domain.Repository;

namespace User.Application.Command.Handler;

public class ChangeProfileDescriptionCommandHandler : IRequestHandler<ChangeProfileDescriptionCommand>
{
    private readonly IUserRepository _userRepository;

    public ChangeProfileDescriptionCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(ChangeProfileDescriptionCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != request.ActionAuthorId && request.ActionAuthorRole != AuthUserRole.Moderator)
        {
            throw new NotAuthorizedException("You are not authorized to change this user description");
        }

        var user = await _userRepository.GetAsync(request.UserId);
        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.ChangeProfileDescription(request.ProfileDescription);  //todo: validate if user can change description (it is user's profile)
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}

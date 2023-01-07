using Domain.Repository;
using MediatR;

namespace Application.Commands.Handlers;

public class AddReactionToWarningCommandHandler : IRequestHandler<AddReactionToWarningCommand>
{
    private readonly IWarningRepository _warningRepository;
    private readonly IUserRepository _userRepository;

    public AddReactionToWarningCommandHandler(IWarningRepository warningRepository, IUserRepository userRepository)
    {
        _warningRepository = warningRepository;
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(AddReactionToWarningCommand request, CancellationToken cancellationToken)
    {
        var warning = await _warningRepository.GetWarningAsync(request.WarningId);
        var user = await _userRepository.GetAsync(request.UserId);

        if (request.Approve is true)
        {
            warning.Approve(user);
        }
        else
        {
            warning.Disapprove(user);
        }

        await _warningRepository.UpdateWarningAsync(warning);
        return Unit.Value;
    }
}

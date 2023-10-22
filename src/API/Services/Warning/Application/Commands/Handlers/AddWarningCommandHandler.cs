using Domain.Entity;
using Domain.Repository;
using MediatR;

namespace Application.Commands.Handlers;

public class AddWarningCommandHandler : IRequestHandler<AddWarningCommand>
{
    private readonly IWarningRepository _warningRepository;
    private readonly IUserRepository _userRepository;

    public AddWarningCommandHandler(IWarningRepository warningRepository, IUserRepository userRepository)
    {
        _warningRepository = warningRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddWarningCommand request, CancellationToken cancellationToken)
    {
        //todo create factory method in doamin to create
        // todo remove Province from request or calc based on lat long coordinates

        var user = await _userRepository.GetAsync(request.AuthorId);
        var warning = new Warning(Guid.NewGuid(), request.Description, request.Province,
            request.MushroomName, request.Latitude, request.Longitude, request.Title, user);
        if (request.AutoActivate)
            warning.Activate();
       
        await _warningRepository.AddWarningAsync(warning);
        return Unit.Value;
    }
}

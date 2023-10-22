using Application.Commands;
using Application.Exception;
using Domain.Repository;
using MediatR;

namespace Post.Application.Command.Handler;
public class ChangeLocationStatusCommandHandler : IRequestHandler<ChangeLocationStatusCommand>
{
    private readonly IWarningRepository _locationRepository;

    public ChangeLocationStatusCommandHandler(IWarningRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Unit> Handle(ChangeLocationStatusCommand request, CancellationToken cancellationToken)
    {
        var location = await _locationRepository.GetWarningAsync(request.LocationId);
        if (location is null)
            throw new ItemNotFoundException("Location has not been found");

        if (request.ChangeToActive)
            location.Activate();
        else
            location.Deactivate();

        await _locationRepository.UpdateWarningAsync(location);
        return Unit.Value;
    }
}

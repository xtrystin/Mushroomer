using Domain.Repository;
using MediatR;

namespace Application.Commands.Handlers;

public class UpdateWarningCommandHandler : IRequestHandler<UpdateWarningCommand>
{
    private readonly IWarningRepository _warningRepository;

    public UpdateWarningCommandHandler(IWarningRepository warningRepository)
    {
        _warningRepository = warningRepository;
    }

    public async Task<Unit> Handle(UpdateWarningCommand request, CancellationToken cancellationToken)
    {
        //todo: validation
        var requestWarning = request.Warning;

        var warning = await _warningRepository.GetWarningAsync(request.Warning.Id);
        warning.Modify(requestWarning.Title, requestWarning.Description, requestWarning.Province,
            requestWarning.MushroomName, requestWarning.Latitude, requestWarning.Longitude,
            requestWarning.Date);

        if (requestWarning.IsActive)
        {
            warning.Activate();
        }
        else
        {
            warning.Deactivate();
        }

        await _warningRepository.UpdateWarningAsync(warning);
        return Unit.Value;
    }
}

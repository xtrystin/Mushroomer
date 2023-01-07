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

        var warning = await _warningRepository.GetWarningAsync(request.Id);
        warning.Modify(request.Title, request.Description, request.Province,
            request.MushroomName, request.Latitude, request.Longitude,
            request.Date);   

        if (request.IsActive)
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

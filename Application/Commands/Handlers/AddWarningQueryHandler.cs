using Domain;
using Domain.Repository;
using MediatR;

namespace Application.Commands.Handlers;

public class AddWarningCommandHandler : IRequestHandler<AddWarningCommand>
{
    private readonly IWarningRepository _warningRepository;

    public AddWarningCommandHandler(IWarningRepository warningRepository)
    {
        _warningRepository = warningRepository;
    }

    public async Task<Unit> Handle(AddWarningCommand request, CancellationToken cancellationToken)
    {
        //todo create factory method in doamin to create
        var requestWarning = request.Warning;
        
        var warning = new Warning(Guid.NewGuid(), requestWarning.Description, requestWarning.Province,
            requestWarning.MushroomName, requestWarning.Latitude, requestWarning.Longitude, requestWarning.Date,
            requestWarning.IsActive, requestWarning.Title);

        await _warningRepository.AddWarningAsync(warning);
        return Unit.Value;
    }
}

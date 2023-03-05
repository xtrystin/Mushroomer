using Domain.Entity;
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
        //todo: only mod and experienced can add

        var warning = new Warning(Guid.NewGuid(), request.Description, request.Province,
            request.MushroomName, request.Latitude, request.Longitude, request.Date,
            request.IsActive, request.Title);

        await _warningRepository.AddWarningAsync(warning);
        return Unit.Value;
    }
}

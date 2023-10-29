using Application.Exception;
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
        // todo remove Province from request or calc based on lat long coordinates

        var warning = await _warningRepository.GetWarningAsync(request.Id);
        if (warning is null)
            throw new ItemNotFoundException("Location has not been found");
        else if (warning.IsAuthor(request.UserId) || request.IsUserMod)
        {
            warning.Modify(request.Title, request.Description, request.Province,
                request.MushroomName, request.Latitude, request.Longitude, request.ThumbnailPhotoUrl);
            
            await _warningRepository.UpdateWarningAsync(warning);
            return Unit.Value;
        }
        else
            throw new NotAuthorizedForOperation("You are not authorized to modify this location");

    }
}

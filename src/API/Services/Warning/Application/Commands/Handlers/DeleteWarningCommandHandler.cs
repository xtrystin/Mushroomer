using Application.Exception;
using Domain.Repository;
using MediatR;

namespace Application.Commands.Handlers;

public class DeleteWarningCommandHandler : IRequestHandler<DeleteWarningCommand>
{
    private readonly IWarningRepository _warningRepository;

    public DeleteWarningCommandHandler(IWarningRepository warningRepository)
    {
        _warningRepository = warningRepository;
    }

    public async Task<Unit> Handle(DeleteWarningCommand request, CancellationToken cancellationToken)
    {
        var warning = await _warningRepository.GetWarningAsync(request.Id);
        if (warning is null)
            throw new ItemNotFoundException("Location has not been found");
        else if (warning.IsAuthor(request.UserId) || request.IsUserMod)
        {
            await _warningRepository.DeleteWarningAsync(warning);
            return Unit.Value;
        }
        else
            throw new NotAuthorizedForOperation("You are not authorized to delete this location");
    }
}

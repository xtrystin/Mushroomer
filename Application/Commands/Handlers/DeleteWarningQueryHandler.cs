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

        await _warningRepository.DeleteWarningAsync(warning);
        return Unit.Value;
    }
}

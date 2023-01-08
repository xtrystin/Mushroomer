using MediatR;
using User.Domain.Repository;

namespace User.Application.Command.Handler;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entity.User(request.Id, request.FirstName, 
            request.LastName, request.EmailAddress, null, null, DateTime.Now);

        await _userRepository.AddAsync(user);
        return Unit.Value;
    }
}

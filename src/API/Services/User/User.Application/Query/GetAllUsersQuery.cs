using MediatR;
using User.Application.ReadModel;

namespace User.Application.Query;

public class GetAllUsersQuery : IRequest<IEnumerable<UserReadModel>>
{
}

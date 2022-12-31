using MediatR;
using User.Application.ReadModel;

namespace User.Application.Query;

public class GetUserQuery : IRequest<UserReadModel>
{
    public Guid Id { get; set; }
}

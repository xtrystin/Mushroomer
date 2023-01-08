using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Application.Query;
using User.Application.ReadModel;
using User.Infrastructure.Ef.Context;

namespace User.Infrastructure.Ef.QueryHandler;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserReadModel>
{
    private readonly ReadUserDbContext _dbContext;

    public GetUserQueryHandler(ReadUserDbContext dbContext)
	{
        _dbContext = dbContext;
    }
    public async Task<UserReadModel> Handle(GetUserQuery request, CancellationToken cancellationToken)  
        => await _dbContext.Users.Include(x => x.Friends).ThenInclude(x => x.Friend).                           //todo: do not include friendToUsers
        Include(x => x.FriendToUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == request.Id);     //todo: handler null
}

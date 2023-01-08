using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Application.Query;
using User.Application.ReadModel;
using User.Infrastructure.Ef.Context;

namespace User.Infrastructure.Ef.QueryHandler;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserReadModel>>
{
    private readonly ReadUserDbContext _dbContext;

    public GetAllUsersQueryHandler(ReadUserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<UserReadModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var a = await _dbContext.Users.Include(x => x.Friends).ThenInclude(x => x.Friend)
            .Include(x => x.FriendToUsers).ThenInclude(x => x.User).ToListAsync();     //todo: handler null
        return a;
    }
        
}

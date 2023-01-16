using UI.ApiLibrary.Dto.User;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IUserEndpoint
{
    Task<UserReadModel> Get(Guid id);
    Task<IEnumerable<UserReadModel>> GetAll();
    Task ChangeFriendship(Guid friendId, bool add);
    Task ChangeProfileDescription(string profileDescription);
}

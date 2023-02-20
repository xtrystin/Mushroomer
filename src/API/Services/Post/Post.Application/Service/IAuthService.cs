namespace Post.Application.Service;

public interface IAuthService
{
    Task<bool> IsUserInRole(Guid userId, string roleName);
}

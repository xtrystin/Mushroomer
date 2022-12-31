using AuthService.Microservices.Dto;

namespace AuthService.Microserives;

public interface IUserMicroservice
{
    Task Post(UserDto user);
}

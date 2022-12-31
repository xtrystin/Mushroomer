using AuthService.Microservices.Dto;

namespace AuthService.Microserives;

public class UserMicroservice : IUserMicroservice
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public UserMicroservice(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }
    public async Task Post(UserDto user)
    {
        var url = _config["MicroservicesUrl:User"] + "/user";

        var response = await _httpClient.PostAsJsonAsync(url, user);     //todo: authorization: server-server code flow
        if (response.IsSuccessStatusCode)
        {
            // log success
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}

using UI.ApiLibrary.Dto.Mushroom;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IMushroomEndpoint
{
    Task<List<Mushroom>> GetAll();
    Task<Mushroom> Get(string id);
    Task Add(MushroomDto mushroom);
    Task Delete(Guid id);
    Task Update(Mushroom mushroom);
}

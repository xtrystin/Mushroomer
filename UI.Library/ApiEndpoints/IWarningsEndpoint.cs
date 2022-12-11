using UI.ApiLibrary.Dto;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IWarningsEndpoint
{
    Task<WarningDto> Get(string id);
    Task<List<WarningDto>> GetAll();
}

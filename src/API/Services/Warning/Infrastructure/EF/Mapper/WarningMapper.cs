using Application.Dto;
using Domain.Entity;

namespace Infrastructure.EF.Mapper;

public class WarningMapper
{
    public static WarningDto MapToDto(Warning warning)
    {
        return new WarningDto(warning.Id, warning.Description, warning.Province, warning.MushroomName,
            warning.Latitude, warning.Longitude, warning.Date, warning.IsActive, warning.Title, 
            warning._reactions, warning.Author?.Email);
    }
}

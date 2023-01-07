﻿using Application.Dto;
using Domain.Entity;

namespace Infrastructure.EF.Mapper;

public class WarningMapper
{
    public static Warning MapFromDto(WarningDto warningDto)     //todo replace with readModels
    {
        return new Warning(warningDto.Id, warningDto.Description, warningDto.Province, warningDto.MushroomName,
            warningDto.Latitude, warningDto.Longitude, warningDto.Date, warningDto.IsActive, warningDto.Title);
    }

    public static WarningDto MapToDto(Warning warning)
    {
        return new WarningDto(warning.Id, warning.Description, warning.Province, warning.MushroomName,
            warning.Latitude, warning.Longitude, warning.Date, warning.IsActive, warning.Title, warning._reactions);
    }
}

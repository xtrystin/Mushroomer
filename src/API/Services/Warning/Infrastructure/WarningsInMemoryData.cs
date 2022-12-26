using Application.Dto;

namespace Infrastructure;

public static class WarningsInMemoryData
{
    public static List<WarningDto> Warnings = new()
    {
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Kania",
            Title = "Warning 0",
            Date = DateTime.Now,
            Latitude = 50.054157,
            Longitude = 19.884258,
            Province = "Podlasie"
        },
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Muchomor samotny",
            Title = "Warning 1",
            Date = DateTime.Now.AddDays(-10),
            Latitude = 50.060663,
            Longitude = 19.848001,
            Province = "Pomorze"
        },
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Muchomor samotny",
            Title = "Warning 2",
            Date = DateTime.Now.AddDays(-10),
            Latitude = 10,
            Longitude = 20,
            Province = "Pomorze"
        },
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Muchomor samotny",
            Title = "Warning 3",
            Date = DateTime.Now.AddDays(-10),
            Latitude = 10,
            Longitude = 20,
            Province = "Pomorze"
        },
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Muchomor samotny",
            Title = "Warning 4",
            Date = DateTime.Now.AddDays(-10),
            Latitude = 10,
            Longitude = 20,
            Province = "Pomorze"
        },
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Muchomor samotny",
            Title = "Warning 5",
            Date = DateTime.Now.AddDays(-10),
            Latitude = 10,
            Longitude = 20,
            Province = "Pomorze"
        },
        new WarningDto()
        {
            Id = Guid.NewGuid(),
            Description = "Warning description.......",
            IsActive = true,
            MushroomName = "Muchomor samotny",
            Title = "Warning 6",
            Date = DateTime.Now.AddDays(-10),
            Latitude = 10,
            Longitude = 20,
            Province = "Pomorze"
        }
    };
}

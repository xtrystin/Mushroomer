using Application.Dto;
using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Dapper.Repository;

public class WarningDapperRepository : IWarningRepository
{
    public Task AddWarningAsync(Warning warning)
    {
        var dbWarning = new WarningDto();
        dbWarning.Title = warning.Title;
        dbWarning.Longitude = warning.Longitude;
        dbWarning.Latitude = warning.Latitude;
        dbWarning.Description = warning.Description;
        dbWarning.IsActive = warning.IsActive;
        dbWarning.MushroomName = warning.MushroomName;
        dbWarning.Date = warning.Date;
        dbWarning.Id = warning.Id;
        dbWarning.Province = warning.Province;

        WarningsInMemoryData.Warnings.Add(dbWarning);
        return Task.CompletedTask;
    }

    public Task DeleteWarningAsync(Warning warning)
    {
        var dbWarning = WarningsInMemoryData.Warnings.First(x => x.Id == warning.Id);
        WarningsInMemoryData.Warnings.Remove(dbWarning);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Warning>> GetAllWarningsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Warning> GetWarningAsync(Guid id)
    {
        var dbWarning = WarningsInMemoryData.Warnings.First(x => x.Id == id);
        var output = new Warning(dbWarning.Id, dbWarning.Description, dbWarning.Province,
            dbWarning.MushroomName, dbWarning.Latitude, dbWarning.Longitude, dbWarning.Date,
            dbWarning.IsActive, dbWarning.Title);

        return Task.FromResult(output);
    }

    public Task UpdateWarningAsync(Warning warning)
    {
        //todo: vakudatuibm error handling, connect to real DB
        var dbWarning = WarningsInMemoryData.Warnings.FirstOrDefault(x => x.Id == warning.Id);
        dbWarning.Title = warning.Title;
        dbWarning.Longitude = warning.Longitude;
        dbWarning.Latitude = warning.Latitude;
        dbWarning.Description = warning.Description;
        dbWarning.IsActive = warning.IsActive;
        dbWarning.MushroomName = warning.MushroomName;
        dbWarning.Date = warning.Date;
        dbWarning.Province = warning.Province;

        return Task.CompletedTask;
    }
}

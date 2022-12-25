namespace Application.Dto;

public class WarningDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Province { get; set; }
    public string MushroomName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public DateTime Date { get; set; }
    public bool IsActive { get; set; }

    public WarningDto(Guid id, string description, string province,
        string mushroomName, double latitude, double longitude,
        DateTime date, bool isActive, string title)
    {
        Id = id;
        Description = description;
        Province = province;
        MushroomName = mushroomName;
        this.Latitude = latitude;
        this.Longitude = longitude;
        Date = date;
        IsActive = isActive;
        Title = title;
    }

    public WarningDto()
    {
    }
}

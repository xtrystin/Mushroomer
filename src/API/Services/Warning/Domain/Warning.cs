using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Warning
{
    public Warning(Guid id, string description, string province,
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

    public Warning()
    {
    }

    public Guid Id { get; init; }   
    public string Title { get; private set; }     
    public string Description { get; private set; }     //todo: primitive obsession!
    public string Province { get; private set; }
    public string MushroomName { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    
    public DateTime Date { get; private set; }
    public bool IsActive { get; private set; }


    public void Modify(string title, string description, string province,
        string mushroomName, double latitude, double longitude,
        DateTime date)
    {
        Title = title;
        Description = description;
        Province = province;
        MushroomName = mushroomName;
        Latitude = latitude;
        Longitude = longitude;
        Date = date;
    }

    public void Activate() => IsActive = true;
    
    public void Deactivate() => IsActive = false;
}

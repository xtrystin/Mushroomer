using Domain.Exception;

namespace Domain.Entity;

public class Warning
{
    public Guid Id { get; init; }
    public string Title { get; private set; }
    public string Description { get; private set; }     //todo: primitive obsession!
    public string Province { get; private set; }
    public string MushroomName { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public DateTime Date { get; private set; }
    public bool IsActive { get; private set; }

    public List<WarningUserReaction> _reactions;        //todo naming violation

    public Warning(Guid id, string description, string province,
    string mushroomName, double latitude, double longitude,
    DateTime date, bool isActive, string title)
    {
        Id = id;
        Description = description;
        Province = province;
        MushroomName = mushroomName;
        Latitude = latitude;
        Longitude = longitude;
        Date = date;
        IsActive = isActive;
        Title = title;
    }

    public Warning()
    {
    }


    public void Modify(string title, string description, string province,
        string mushroomName, double latitude, double longitude,
        DateTime date, List<WarningUserReaction> reactions = null)
    {
        Title = title;
        Description = description;
        Province = province;
        MushroomName = mushroomName;
        Latitude = latitude;
        Longitude = longitude;
        Date = date;
        _reactions = reactions;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void Approve(User user)
    {
        var reaction = _reactions.FirstOrDefault(x => x.WarningId == Id && x.UserId == user.Id);

        if (reaction is not null && reaction.Approve is true)
        {
            throw new UserAlreadyReactedToWarning("You have already approved this warning");
        }
        else if (reaction is not null)
        {
            reaction.Approve = true;
        }
        else
        {
            var newreaction = new WarningUserReaction
            {
                Id = default,
                UserId = user.Id,
                WarningId = Id,
                Approve = true,
                User = user,
                Warning = this
            };

            _reactions.Add(newreaction);
        }
    }

    public void Disapprove(User user)
    {
        var reaction = _reactions.FirstOrDefault(x => x.WarningId == Id && x.UserId == user.Id);

        if (reaction is not null && reaction.Approve is false)
        {
            throw new UserAlreadyReactedToWarning("You have already disapproved this warning");
        }
        else if (reaction is not null)
        {
            reaction.Approve = false;
        }
        else
        {
            var newreaction = new WarningUserReaction
            {
                Id = default,
                UserId = user.Id,
                WarningId = Id,
                Approve = false,
                User = user,
                Warning = this
            };

            _reactions.Add(newreaction);
        }
    }
}

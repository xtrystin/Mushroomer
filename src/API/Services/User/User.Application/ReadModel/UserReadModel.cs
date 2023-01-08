using System.Text.Json.Serialization;

namespace User.Application.ReadModel;

public class UserReadModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string? ProfileDescription { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime CreatedDate { get; set; }

    public List<FriendDto> FriendsList
    { 
        get 
        {
            var output = Friends?.Select(x => new FriendDto()
            {
                Id = x.FriendId,
                FirstName = x.Friend.FirstName,
                LastName = x.Friend.LastName,
                EmailAddress = x.Friend.EmailAddress,
                PhotoUrl = x.Friend.PhotoUrl,
                ProfileDescription = x.Friend.ProfileDescription,
                FriendshipStartedDate = CreatedDate,
                CreatedDate = x.Friend.CreatedDate
            }).ToList();

            return output;
        }
    }

    [JsonIgnore]
    public List<UserFriendReadModel> Friends { get; set; }
    
    [JsonIgnore]
    public List<UserFriendReadModel> FriendToUsers { get; set; }
}

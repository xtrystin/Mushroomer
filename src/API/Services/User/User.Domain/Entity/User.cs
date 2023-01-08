using User.Domain.Exception;
using User.Domain.ValueObject;

namespace User.Domain.Entity;

public class User
{
    public UserId Id { get; private set; }

    private FirstName _firstName;
    private LastName _lastName;
    private EmailAddress _emailAddress;
    private ProfileDescription? _profileDescription;
    private PhotoUrl? _photoUrl;
    private DateTime _createdDate;

    private List<UserFriend> _friends;
    private List<UserFriend> _friendToUsers;

    public User(UserId id, FirstName firstName, LastName lastName, EmailAddress emailAddress, ProfileDescription? profileDescription, PhotoUrl? photoUrl, DateTime createdDate)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _emailAddress = emailAddress;
        _profileDescription = profileDescription;
        _photoUrl = photoUrl;
        _createdDate = createdDate;
    }

    public User()
    {
    }

    public void ChangeFirstName(FirstName firstName) => _firstName = firstName;
    public void ChangeProfileDescription(ProfileDescription description) => _profileDescription = description;

    public void AddFriend(User friend)
    {
        if (Id == friend.Id)
        {
            throw new SelfAddToFriendsException();
        }
        var friendInfo = _friends.FirstOrDefault(x => x.UserId == Id && x.FriendId == friend.Id);
        if (friendInfo is not null)
        {
            throw new IsFriendAlreadyException();
        }

        var newFriend = new UserFriend() { Id = default, UserId = Id, CreatedDate = DateTime.Now,
            User = this, FriendId = friend.Id, Friend = friend };
        
        _friends.Add(newFriend);
    }

    public void RemoveFriend(User friend)
    {
        var friendInfo = _friends.FirstOrDefault(x => x.UserId == Id && x.FriendId == friend.Id);
        if (friendInfo is null)
        {
            throw new UserOsNotFriendException();
        }

        _friends.Remove(friendInfo);
    }

    //todo 223: this is hack for problems with deleting row in many to self many relation, EF wants to set null to foreign key column isntead of deleting the whole row
    public void RemoveFriendToUsers(User user)
    {
        var friendInfo = _friendToUsers.FirstOrDefault(x => x.FriendId == Id && x.UserId == user.Id);
        if (friendInfo is null)
        {
            throw new UserOsNotFriendException();
        }

        _friendToUsers.Remove(friendInfo);
    }
}

namespace UI.ApiLibrary.Dto.User;

public class UserReadModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string? ProfileDescription { get; set; }
    public string? PhotoUrl { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<FriendDto> FriendsList { get; set; }
}

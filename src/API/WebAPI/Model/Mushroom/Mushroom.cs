namespace WebAPI.Model.Mushroom;

public class Mushroom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsPoisonous { get; set; }
    public string PhotoUrl { get; set; }
}

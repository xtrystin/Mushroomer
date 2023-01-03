using Post.Application.Dto;
using System.Text.Json.Serialization;

namespace Post.Application.ReadModel;

public class PostUserReactionReadModel
{
    public Guid Id { get; set; }
    public bool Like { get; set; } // todo: enum to have more reactions: happy, sad, like, dislikehttps://stackoverflow.com/questions/1434298/sql-server-equivalent-to-mysql-enum-data-type

    [JsonIgnore]
    public UserReadModel User { get; set; }
    public Guid UserId { get; set; }
    
    [JsonIgnore]
    public PostReadModel Post { get; set; }
    public Guid PostId { get; set; }
}

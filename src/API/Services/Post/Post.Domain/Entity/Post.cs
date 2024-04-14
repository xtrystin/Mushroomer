using Post.Domain.Exception;
using Post.Domain.ValueObject;

namespace Post.Domain.Entity;

public class Post
{
    public PostId Id { get; private set; }

    private PostTitle _title;
    private PostContent _content;
    private DateTime _createdDate;
    private DateTime _lastModificationDate;
    private readonly List<Comment> _comments;
    private User _author;
    private List<PostUserReaction> _reactions = new();
    private bool _isActive;
    private ThumbnailPhotoUrl _thumbnailPhotoUrl;


    public Post(PostId id, PostTitle title, PostContent content, List<Comment> comments, User author, ThumbnailPhotoUrl thumbnailPhotoUrl)
    {
        Id = id;
        _title = title;
        _content = content;
        _createdDate = DateTime.Now;
        _lastModificationDate = _createdDate;
        _comments = comments;
        _author = author;
        _isActive = false;
        _thumbnailPhotoUrl = thumbnailPhotoUrl;
    }

    public Post()
    {

    }

    public void Activate() => _isActive = true;
    public void Deactivate() => _isActive = false;

    public void ChangeTitle(PostTitle title)
    {
        _title = title;
        _lastModificationDate = DateTime.Now;
    }

    public void ChangeContent(PostContent content) 
    {
        _content = content;
        _lastModificationDate = DateTime.Now;
    }

    public void ChangeThumbnailPhotoUrl(ThumbnailPhotoUrl thumbnailPhotoUrl)
    {
        _thumbnailPhotoUrl = thumbnailPhotoUrl;
        _lastModificationDate = DateTime.Now;
    }

    public bool IsAuthor(Guid userId)
        => _author.Id == userId;

    public void AddComment(Comment comment) 
    {
        _comments.Add(comment);
    }

    [Obsolete]
    public void DeleteComment(Guid commentId)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId);
        _comments.Remove(comment);
    }

    public void ModifyComment(Guid commentId, string content)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId);
        comment.ModifyContent(content);
    }

    public bool IsCommentAuthor(Guid commentId, Guid userId)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId);
        return comment.IsCommentAuthor(userId);
    }

    [Obsolete]
    public void ChangeCommentContent(Guid commentId, string content)
    {
        _comments.FirstOrDefault(x => x.Id == commentId).ModifyContent(content);
    }

    public void RemoveComment(Guid commentId)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId);
        _comments.Remove(comment);
    }

    public List<Comment> GetComments()
        => _comments;

    public void Like(User user)
    {
        var reaction = _reactions.FirstOrDefault(x => x.PostId == Id && x.UserId == user.Id);

        if (reaction is not null && reaction.Like is true)
        {
            throw new UserAlreadyReactedToPost("You have already liked this post");
        }
        else if (reaction is not null)
        {
            reaction.Like = true;
        }
        else
        {
            var newreaction = new PostUserReaction { Id = default, UserId = user.Id, 
                PostId = Id, Like = true, User = user, Post = this};
            
            _reactions.Add(newreaction);
        }
    }

    public void DisLike(User user)
    {
        var reaction = _reactions.FirstOrDefault(x => x.PostId == Id && x.UserId == user.Id);

        if (reaction is not null && reaction.Like is false)
        {
            throw new UserAlreadyReactedToPost("You have already disliked this post");
        }
        else if (reaction is not null)
        {
            reaction.Like = false;
        }
        else
        {
            var newreaction = new PostUserReaction { Id = default, UserId = user.Id, 
                PostId = Id, Like = false, User = user, Post = this};
            
            _reactions.Add(newreaction);
        }
    }
}

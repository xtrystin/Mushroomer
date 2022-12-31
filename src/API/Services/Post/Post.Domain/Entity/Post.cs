using Post.Domain.ValueObject;

namespace Post.Domain.Entity;

public class Post
{
    public PostId Id { get; private set; }

    private PostTitle _title;
    private PostContent _content;
    private DateTime _createdDate;
    private DateTime _lastModificationDate;
    private readonly List<Comment> _comments;   // todo: move to new entity CommentList
    private User _author;


    public Post(PostId id, PostTitle title, PostContent content, DateTime date, List<Comment> comments, User author)
    {
        Id = id;
        _title = title;
        _content = content;
        _createdDate = date;
        _lastModificationDate = date;
        _comments = comments;
        _author = author;
    }

    public Post()
    {

    }

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

    public void AddComment(Comment comment) 
    {
        _comments.Add(comment);
    }

    public void DeleteComment(Comment comment)
    {
        _comments.Remove(comment);
    }

    public void ChangeCommentContent(Guid commentId, string content)
    {
        _comments.First(x => x.Id == commentId).ModifyContent(content);
    }

    public List<Comment> GetComments()  //todo add notFound exception 
        => _comments;

    public void Like(Guid UserId)
    {
        // check if user has already liked this post

    }
}

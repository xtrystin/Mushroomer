using Post.Domain.Entity;
using Post.Domain.Exception;
using Post.Domain.ValueObject;
using System.Reflection;

namespace Post.Tests;

[TestFixture]
public class PostTests
{
    [Test]
    public void ChangeTitle_UpdatesTitleAndLastModificationDate()
    {
        // Arrange
        var postId = new PostId(Guid.NewGuid());
        var post = new Domain.Entity.Post(postId, new PostTitle("Old Title"), new PostContent("Content"), new List<Comment>(), new User(), null);
        var title = typeof(Domain.Entity.Post).GetField("_title", BindingFlags.NonPublic | BindingFlags.Instance);
        var lastModificationDate = typeof(Domain.Entity.Post).GetField("_lastModificationDate", BindingFlags.NonPublic | BindingFlags.Instance);
        

        // Act
        post.ChangeTitle(new PostTitle("New Title"));

        // Assert
        var actualTitle = ((PostTitle)title.GetValue(post)).Value;
        var actualLastModificationDate = ((DateTime)lastModificationDate.GetValue(post));
        Assert.AreEqual("New Title", actualTitle);
        Assert.That(actualLastModificationDate, Is.EqualTo(DateTime.Now).Within(1).Seconds);
    }

    [Test]
    public void ChangeContent_UpdatesContentAndLastModificationDate()
    {
        // Arrange
        var postId = new PostId(Guid.NewGuid());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Old Content"), new List<Comment>(), new User(), null);
        var content = typeof(Domain.Entity.Post).GetField("_content", BindingFlags.NonPublic | BindingFlags.Instance);
        var lastModificationDate = typeof(Domain.Entity.Post).GetField("_lastModificationDate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        post.ChangeContent(new PostContent("New Content"));

        // Assert
        var actualLastModificationDate = ((DateTime)lastModificationDate.GetValue(post));
        var actualContent = ((PostContent)content.GetValue(post)).Value;

        Assert.AreEqual("New Content", actualContent);
        Assert.That(actualLastModificationDate, Is.EqualTo(DateTime.Now).Within(1).Seconds);
    }

    [Test]
    public void ChangeThumbnailPhotoUrl_UpdatesThumbnailPhotoUrlAndLastModificationDate()
    {
        // Arrange
        var postId = new PostId(Guid.NewGuid());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), new User(), new ThumbnailPhotoUrl("https://OldUrl"));
        var thumbnailPhotoUrl = typeof(Domain.Entity.Post).GetField("_thumbnailPhotoUrl", BindingFlags.NonPublic | BindingFlags.Instance);
        var lastModificationDate = typeof(Domain.Entity.Post).GetField("_lastModificationDate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        post.ChangeThumbnailPhotoUrl(new ThumbnailPhotoUrl("https://NewUrl"));

        // Assert
        var actualLastModificationDate = ((DateTime)lastModificationDate.GetValue(post));
        var actualThumbnailPhotoUrl = ((ThumbnailPhotoUrl)thumbnailPhotoUrl.GetValue(post)).Value;

        Assert.AreEqual("https://NewUrl", actualThumbnailPhotoUrl);
        Assert.That(actualLastModificationDate, Is.EqualTo(DateTime.Now).Within(1).Seconds);
    }

    [Test]
    public void IsAuthor_ReturnsTrueIfUserIdMatchesAuthorId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var postId = new PostId(Guid.NewGuid());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);

        // Act
        var isAuthor = post.IsAuthor(userId);

        // Assert
        Assert.IsTrue(isAuthor);
    }

    [Test]
    public void IsAuthor_ReturnsFalseIfUserIdDoesNotMatchAuthorId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User(Guid.NewGuid(), "Jane", "Doe", "jane@example.com", new List<Domain.Entity.Post>());
        var postId = new PostId(Guid.NewGuid());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);

        // Act
        var isAuthor = post.IsAuthor(userId);

        // Assert
        Assert.IsFalse(isAuthor);
    }


    [Test]
    public void AddComment_AddsCommentToList()
    {
        // Arrange
        var postId = new PostId(Guid.NewGuid());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), new User(), null);
        var comment = new Comment();

        // Act
        post.AddComment(comment);

        // Assert
        Assert.Contains(comment, post.GetComments());
    }

    [Test]
    public void DeleteComment_RemovesCommentFromList()
    {
        // Arrange
        var postId = new PostId(Guid.NewGuid());
        var commentId = Guid.NewGuid();
        var comment = new Comment(commentId, "Content", DateTime.Now, postId, new Domain.Entity.Post(), new User());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment> { comment }, new User(), null);

        // Act
        post.RemoveComment(commentId);

        // Assert
        Assert.IsEmpty(post.GetComments());
    }

    [Test]
    public void ModifyComment_UpdatesCommentContentAndLastModificationDate()
    {
        // Arrange
        var postId = new PostId(Guid.NewGuid());
        var commentId = Guid.NewGuid();
        var comment = new Comment(commentId, "Old Content", DateTime.Now, postId, new Domain.Entity.Post(), new User());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment> { comment }, new User(), null);
        
        var content = typeof(Domain.Entity.Comment).GetField("_content", BindingFlags.NonPublic | BindingFlags.Instance);
        var lastModificationDate = typeof(Domain.Entity.Comment).GetField("_lastModificationDate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var newContent = "New Content";
        post.ModifyComment(commentId, newContent);

        // Assert
        var actualLastModificationDate = ((DateTime)lastModificationDate.GetValue(comment));
        var actualContent = ((string)content.GetValue(comment));

        Assert.AreEqual("New Content", actualContent);
        Assert.That(actualLastModificationDate, Is.EqualTo(DateTime.Now).Within(1).Seconds);

        Assert.AreEqual(newContent, actualContent);
        Assert.That(actualLastModificationDate, Is.EqualTo(DateTime.Now).Within(1).Seconds);
    }


    [Test]
    public void Like_AddsLikeReaction_WhenUserHasNotAlreadyReacted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var postId = new PostId(Guid.NewGuid());
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);

        var reactions = typeof(Domain.Entity.Post).GetField("_reactions", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        post.Like(user);

        // Assert
        var actualReactions = (List<PostUserReaction>)reactions.GetValue(post);
        Assert.IsTrue(actualReactions.Exists(r => r.UserId == userId && r.Like));
    }
    
    [Test]
    public void Like_ThrowsException_WhenUserHasAlreadyLiked()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var postId = new PostId(Guid.NewGuid());
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);
        post.Like(user); // User already liked

        // Act & Assert
        Assert.Throws<UserAlreadyReactedToPost>(() => post.Like(user));
    }

    [Test]
    public void Like_UpdatesExistingReaction_WhenUserHasDislikedPreviously()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var postId = new PostId(Guid.NewGuid());
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);
        
        var reactions = typeof(Domain.Entity.Post).GetField("_reactions", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        post.DisLike(user);
        post.Like(user);

        // Assert
        var actualReactions = (List<PostUserReaction>)reactions.GetValue(post);
        Assert.IsTrue(actualReactions.Exists(r => r.UserId == userId && r.Like));
    }


    [Test]
    public void DisLike_AddsDisLikeReaction_WhenUserHasNotAlreadyReacted()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var postId = new PostId(Guid.NewGuid());
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);

        var reactions = typeof(Domain.Entity.Post).GetField("_reactions", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        post.DisLike(user);

        // Assert
        var actualReactions = (List<PostUserReaction>)reactions.GetValue(post);
        Assert.IsTrue(actualReactions.Exists(r => r.UserId == userId && r.Like == false));
    }
    
    [Test]
    public void DisLike_ThrowsException_WhenUserHasAlreadyDisLiked()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var postId = new PostId(Guid.NewGuid());
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);
        post.DisLike(user); // User already liked

        // Act & Assert
        Assert.Throws<UserAlreadyReactedToPost>(() => post.DisLike(user));
    }

    [Test]
    public void DisLike_UpdatesExistingReaction_WhenUserHasLikedPreviously()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var postId = new PostId(Guid.NewGuid());
        var user = new User(userId, "John", "Doe", "john@example.com", new List<Domain.Entity.Post>());
        var post = new Domain.Entity.Post(postId, new PostTitle("Title"), new PostContent("Content"), new List<Comment>(), user, null);
        
        var reactions = typeof(Domain.Entity.Post).GetField("_reactions", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        post.Like(user);
        post.DisLike(user);

        // Assert
        var actualReactions = (List<PostUserReaction>)reactions.GetValue(post);
        Assert.IsTrue(actualReactions.Exists(r => r.UserId == userId && r.Like == false));
    }
}

using Post.Domain.Exception;
using Post.Domain.ValueObject;

namespace Post.Tests;


[TestFixture]
public class PostIdTests
{
    [Test]
    public void Constructor_ValidId_ShouldCreatePostId()
    {
        // Arrange
        Guid validId = Guid.NewGuid();

        // Act
        var postId = new PostId(validId);

        // Assert
        Assert.AreEqual(validId, postId.Value);
    }

    [Test]
    public void Constructor_EmptyId_ShouldThrowEmptyPostIdException()
    {
        // Arrange
        Guid emptyId = Guid.Empty;

        // Act & Assert
        Assert.Throws<EmptyPostIdException>(() => new PostId(emptyId));
    }

    [Test]
    public void ImplicitConversion_FromPostIdToGuid_ShouldReturnCorrectGuid()
    {
        // Arrange
        Guid validId = Guid.NewGuid();
        var postId = new PostId(validId);

        // Act
        Guid result = postId;

        // Assert
        Assert.AreEqual(validId, result);
    }

    [Test]
    public void ImplicitConversion_FromGuidToPostId_ShouldCreatePostId()
    {
        // Arrange
        Guid validId = Guid.NewGuid();

        // Act
        PostId postId = validId;

        // Assert
        Assert.AreEqual(validId, postId.Value);
    }
}
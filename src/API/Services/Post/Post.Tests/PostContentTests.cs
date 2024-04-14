using Post.Domain.Exception;
using Post.Domain.ValueObject;

namespace Post.Tests;

[TestFixture]
public class PostContentTests
{
    [Test]
    public void Constructor_ValidContent_ShouldCreatePostContent()
    {
        // Arrange
        string validContent = "This is a valid post content.";

        // Act
        var postContent = new PostContent(validContent);

        // Assert
        Assert.AreEqual(validContent, postContent.Value);
    }

    [Test]
    public void Constructor_NullContent_ShouldThrowInvalidPostContentException()
    {
        // Arrange
        string nullContent = null;

        // Act & Assert
        Assert.Throws<InvalidPostContentException>(() => new PostContent(nullContent));
    }

    [Test]
    public void Constructor_EmptyContent_ShouldThrowInvalidPostContentException()
    {
        // Arrange
        string emptyContent = string.Empty;

        // Act & Assert
        Assert.Throws<InvalidPostContentException>(() => new PostContent(emptyContent));
    }

    [Test]
    public void Constructor_ContentExceedsMaxLength_ShouldThrowInvalidPostContentException()
    {
        // Arrange
        string longContent = new string('A', 10001);

        // Act & Assert
        Assert.Throws<InvalidPostContentException>(() => new PostContent(longContent));
    }

    [Test]
    public void ImplicitConversion_FromPostContentToString_ShouldReturnCorrectString()
    {
        // Arrange
        string validContent = "This is a valid post content.";
        var postContent = new PostContent(validContent);

        // Act
        string result = postContent;

        // Assert
        Assert.AreEqual(validContent, result);
    }

    [Test]
    public void ImplicitConversion_FromStringToPostContent_ShouldCreatePostContent()
    {
        // Arrange
        string validContent = "This is a valid post content.";

        // Act
        PostContent postContent = validContent;

        // Assert
        Assert.AreEqual(validContent, postContent.Value);
    }
}

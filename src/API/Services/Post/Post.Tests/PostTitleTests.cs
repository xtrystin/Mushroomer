using Post.Domain.Exception;
using Post.Domain.ValueObject;

namespace Post.Tests;

[TestFixture]
public class PostTitleTests
{
    [Test]
    public void Constructor_ValidTitle_ShouldCreatePostTitle()
    {
        // Arrange
        string validTitle = "This is a valid post title";

        // Act
        var postTitle = new PostTitle(validTitle);

        // Assert
        Assert.AreEqual(validTitle, postTitle.Value);
    }

    [Test]
    public void Constructor_NullTitle_ShouldThrowInvalidPostTitleException()
    {
        // Arrange
        string nullTitle = null;

        // Act & Assert
        Assert.Throws<InvalidPostTitleException>(() => new PostTitle(nullTitle));
    }

    [Test]
    public void Constructor_EmptyTitle_ShouldThrowInvalidPostTitleException()
    {
        // Arrange
        string emptyTitle = string.Empty;

        // Act & Assert
        Assert.Throws<InvalidPostTitleException>(() => new PostTitle(emptyTitle));
    }

    [Test]
    public void Constructor_TitleExceedsMaxLength_ShouldThrowInvalidPostTitleException()
    {
        // Arrange
        string longTitle = new string('A', 201);

        // Act & Assert
        Assert.Throws<InvalidPostTitleException>(() => new PostTitle(longTitle));
    }

    [Test]
    public void ImplicitConversion_FromPostTitleToString_ShouldReturnCorrectString()
    {
        // Arrange
        string validTitle = "This is a valid post title";
        var postTitle = new PostTitle(validTitle);

        // Act
        string result = postTitle;

        // Assert
        Assert.AreEqual(validTitle, result);
    }

    [Test]
    public void ImplicitConversion_FromStringToPostTitle_ShouldCreatePostTitle()
    {
        // Arrange
        string validTitle = "This is a valid post title";

        // Act
        PostTitle postTitle = validTitle;

        // Assert
        Assert.AreEqual(validTitle, postTitle.Value);
    }
}
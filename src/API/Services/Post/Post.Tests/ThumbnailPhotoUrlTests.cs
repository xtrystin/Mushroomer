using Post.Domain.Exception;
using Post.Domain.ValueObject;

namespace Post.Tests;

[TestFixture]
public class ThumbnailPhotoUrlTests
{
    [Test]
    public void Constructor_WithValidUrl_ReturnsThumbnailPhotoUrlInstance()
    {
        // Arrange
        string validUrl = "http://example.com/image.jpg";

        // Act
        ThumbnailPhotoUrl thumbnailPhotoUrl = new ThumbnailPhotoUrl(validUrl);

        // Assert
        Assert.AreEqual(validUrl, thumbnailPhotoUrl.Value);
    }

    [Test]
    public void Constructor_WithNullValue_ReturnsThumbnailPhotoUrlInstanceWithNullValue()
    {
        // Act
        ThumbnailPhotoUrl thumbnailPhotoUrl = new ThumbnailPhotoUrl(null);

        // Assert
        Assert.IsNull(thumbnailPhotoUrl.Value);
    }

    [Test]
    public void Constructor_WithEmptyString_ReturnsThumbnailPhotoUrlInstanceWithEmptyValue()
    {
        // Act
        ThumbnailPhotoUrl thumbnailPhotoUrl = new ThumbnailPhotoUrl(string.Empty);

        // Assert
        Assert.AreEqual(string.Empty, thumbnailPhotoUrl.Value);
    }

    [Test]
    public void Constructor_WithInvalidUrl_ThrowsInvalidThumbnailPhotoUrlException()
    {
        // Arrange
        string invalidUrl = "invalid-url";

        // Assert
        Assert.Throws<InvalidThumbnailPhotoUrl>(() => new ThumbnailPhotoUrl(invalidUrl));
    }

    [Test]
    public void Constructor_WithUrlExceedingMaxLength_ThrowsInvalidThumbnailPhotoUrlException()
    {
        // Arrange
        string longUrl = new string('a', ThumbnailPhotoUrl.MaxLength + 1);

        // Assert
        Assert.Throws<InvalidThumbnailPhotoUrl>(() => new ThumbnailPhotoUrl(longUrl));
    }

    [Test]
    public void ImplicitConversion_FromThumbnailPhotoUrlToString_ReturnsValue()
    {
        // Arrange
        string validUrl = "http://example.com/image.jpg";
        ThumbnailPhotoUrl thumbnailPhotoUrl = new ThumbnailPhotoUrl(validUrl);

        // Act
        string result = thumbnailPhotoUrl;

        // Assert
        Assert.AreEqual(validUrl, result);
    }

    [Test]
    public void ImplicitConversion_FromStringToThumbnailPhotoUrl_ReturnsInstance()
    {
        // Arrange
        string validUrl = "http://example.com/image.jpg";

        // Act
        ThumbnailPhotoUrl thumbnailPhotoUrl = validUrl;

        // Assert
        Assert.AreEqual(validUrl, thumbnailPhotoUrl.Value);
    }
}

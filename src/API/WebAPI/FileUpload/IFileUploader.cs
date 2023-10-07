namespace WebApplication1;

public interface IFileUploader
{
    Task<string> UploadImageFileAsync(Stream fileStream, string fileName);
}
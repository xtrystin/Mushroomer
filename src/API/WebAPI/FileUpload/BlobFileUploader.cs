using Azure.Storage.Blobs;

namespace WebApplication1;

public class BlobFileUploader : IFileUploader
{
    private string _sasToken;
    private string _accountName;
    private string _imageContainerName;

    public BlobFileUploader(IConfiguration configuration)
    {
        _accountName = configuration["BlobStorage:AccountName"];
        _imageContainerName = configuration["BlobStorage:ImageContainerName"];
        _sasToken = configuration["BlobStorage:SasToken"];
    }

    private static BlobServiceClient GetBlobServiceClientSAS(string accountName, string sasToken)
    {
        string blobUri = "https://" + accountName + ".blob.core.windows.net";
        return new BlobServiceClient(new Uri($"{blobUri}?{sasToken}"), null);
    }

    private static BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
    {
        return blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<string> UploadImageFileAsync(Stream fileStream, string fileName)
    {
        string folder = $"{DateTime.Today.ToString("yyyy-MM-dd")}";
        fileName = $"{Guid.NewGuid()}-{fileName}";

        var blockServiceClient = GetBlobServiceClientSAS(_accountName, _sasToken);
        var blockContainerClient = GetBlobContainerClient(blockServiceClient, _imageContainerName);
        BlobClient blobClient = blockContainerClient.GetBlobClient(folder + "/" + fileName);

        await blobClient.UploadAsync(fileStream, true);
        string fileUrl = $"https://{_accountName}.blob.core.windows.net/{_imageContainerName}/{folder}/{fileName}";
        return fileUrl;
    }
}

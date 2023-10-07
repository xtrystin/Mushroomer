using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
public class UploadController : ControllerBase
{
    private readonly IFileUploader _fileUploader;
    private bool _isFileUploadEnabled = false;

    public UploadController(IFileUploader fileUploader, IConfiguration configuration)
    {
        _fileUploader = fileUploader;
        _isFileUploadEnabled = configuration["Flags:IsFileUploadEnabled"] == "1" ? true : false;
    }

    [HttpPost("upload/image")]
    public async Task<IActionResult> Image(IFormFile file)
    {
        if (_isFileUploadEnabled is false)
            return StatusCode(500, "File upload feature is not currently available");
        
        try
        {
            var fileUrl = await _fileUploader.UploadImageFileAsync(file.OpenReadStream(), file.FileName);
            return Ok(new { Url = fileUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error occurred while uploading file");
        }
    }
}

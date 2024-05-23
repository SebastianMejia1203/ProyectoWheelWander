using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoWheelWander.Services;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IUploadImageService _uploadImageService;

    public UploadController(IUploadImageService uploadImageService)
    {
        _uploadImageService = uploadImageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is null or empty.");
        }

        var url = await _uploadImageService.UploadImageAsync(file);
        return Ok(new { Url = url });
    }
}

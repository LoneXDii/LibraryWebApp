using Microsoft.AspNetCore.Mvc;
using LibraryServer.Application.Services.StorageServices.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IBlobService _blobService;

    public FilesController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    public async Task<ActionResult<Guid>> SaveFile(IFormFile file)
    {
        using Stream stream = file.OpenReadStream();

        Guid fileId = await _blobService.UploadAsync(stream, file.ContentType);
        return Ok(fileId);
    }

    [HttpGet("{fileId}")]
    public async Task<FileStreamResult> GetFile(Guid fileId)
    {
        var response = await _blobService.DownloadAsync(fileId);
        return File(response.Stream, response.ContentType);
    }

    [HttpDelete("{fileId}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteFile(Guid fileId)
    {
        await _blobService.DeleteAsync(fileId);
        return Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public FilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    public async Task<ActionResult<Guid>> SaveFile(IFormFile file)
    {
        Guid fileId = await _mediator.Send(new SaveFileRequest(file));
        return Ok(fileId);
    }

    [HttpGet("{fileId}")]
    public async Task<FileStreamResult> GetFile(Guid fileId)
    {
        var response = await _mediator.Send(new GetFileRequest(fileId));
        return File(response.Stream, response.ContentType);
    }

    [HttpDelete("{fileId}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteFile(Guid fileId)
    {
        await _mediator.Send(new DeleteFileRequest(fileId));
        return Ok();
    }
}

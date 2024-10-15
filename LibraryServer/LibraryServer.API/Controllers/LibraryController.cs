using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibraryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    [Route("take")]
    public async Task<IActionResult> GiveBook(GiveOrTakeBookDTO giveBook)
    {
        giveBook.UserId = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        await _mediator.Send(new GiveBookToUserRequest(giveBook));
        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("user-books")]
    public async Task<ActionResult<List<TakenBookDTO>>> GetUserBooks()
    {
        var userId = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var response = await _mediator.Send(new ListUsersTakenBooksRequest(userId));
        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = "admin")]
    [Route("user-books/{id}")]
    public async Task<ActionResult<List<TakenBookDTO>>> GetUserBooks(string id)
    {
        var response = await _mediator.Send(new ListUsersTakenBooksRequest(id));
        return Ok(response);
    }
}

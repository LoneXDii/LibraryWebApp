using LibraryServer.Application.DTO;
using LibraryServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly IBookService _bookService;

    public LibraryController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    [Authorize]
    [Route("take")]
    public async Task<IActionResult> TakeBook(TakeBookDTO request)
    {
        await _bookService.GiveToUserAsync(request.BookId, request.UserId);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("user-books/{id}")]
    public async Task<ActionResult<List<TakenBookDTO>>> GetUserBooks(string id)
    {
        var response = await _bookService.GetUserBooksAsync(id);
        return Ok(response);
    }
}

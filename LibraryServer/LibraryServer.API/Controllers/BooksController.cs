using LibraryServer.Application.DTO;
using LibraryServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _booksService;

    public BooksController(IBookService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    [Route("{genre?}")]
    public async Task<ActionResult<List<BookDTO>>> GetBooks(string? genre, int pageNo = 1,
                                                            int pageSize = 9)
    {
        return Ok(await _booksService.ListAsync(genre, pageNo, pageSize));
    }
}

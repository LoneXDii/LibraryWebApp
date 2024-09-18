using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
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
    public async Task<ActionResult<ResponseData<List<BookDTO>>>> GetBooks(string? genre, int pageNo = 1,
                                                                          int pageSize = 9)
    {
        var response = await _booksService.ListAsync(genre, pageNo, pageSize);
        if (!response.Successfull)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}

using LibraryServer.Application.DTO;
using LibraryServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [Route("{genre?}")]
    public async Task<ActionResult<List<BookDTO>>> GetBooks(string? genre, int pageNo = 1,
                                                            int pageSize = 9)
    {
        return Ok(await _bookService.ListAsync(genre, pageNo, pageSize));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDTO>> GetBookById(int id)
    {
        return Ok(await _bookService.GetByIdAsync(id));
    }

    [HttpGet]
    [Route("isbn/{isbn}")]
    public async Task<ActionResult<BookDTO>> GetBookByISBN(string isbn)
    {
        return Ok(await _bookService.FirstOrDefaultAsync(b => b.ISBN == isbn));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutGenre(int id, BookDTO genre)
    {
        await _bookService.UpdateAsync(id, genre);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<BookDTO>> PostGenre(BookDTO genre)
    {
        return Ok(await _bookService.AddAsync(genre));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _bookService.DeleteAsync(id);
        return Ok();
    }
}

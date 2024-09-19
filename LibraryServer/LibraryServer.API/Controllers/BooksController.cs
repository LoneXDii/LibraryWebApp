using LibraryServer.Application.DTO;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Domain.Common.Models;
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
    public async Task<ActionResult<List<PaginatedListModel<BookDTO>>>> GetBooks(string? genre, int pageNo = 1,
                                                                                int pageSize = 9)
    {
        var resposne = await _bookService.ListAsync(genre, pageNo, pageSize);
        return Ok(resposne);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDTO>> GetBookById(int id)
    {
        var response = await _bookService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpGet]
    [Route("isbn/{isbn}")]
    public async Task<ActionResult<BookDTO>> GetBookByISBN(string isbn)
    {
        var response = await _bookService.FirstOrDefaultAsync(b => b.ISBN == isbn);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutBook(int id, BookDTO book)
    {
        await _bookService.UpdateAsync(id, book);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<BookDTO>> PostBook(BookDTO book)
    {
        var response = await _bookService.AddAsync(book);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteAsync(id);
        return Ok();
    }
}

using LibraryServer.Domain.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{genre?}")]
    public async Task<ActionResult<List<PaginatedListModel<BookDTO>>>> GetBooks(string? genre, int pageNo = 1,
                                                                                int pageSize = 9)
    {
        var resposne = await _mediator.Send(new ListBooksWithPaginationRequest(genre, pageNo, pageSize));
        return Ok(resposne);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDTO>> GetBookById(int id)
    {
        var response = await _mediator.Send(new GetBookByIdRequest(id));
        return Ok(response);
    }

    [HttpGet]
    [Route("isbn/{isbn}")]
    public async Task<ActionResult<BookDTO>> GetBookByISBN(string isbn)
    {
        var response = await _mediator.Send(new GetFirstOrDefaultBookRequest(b => b.ISBN == isbn));
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> PutBook(int id, [FromForm] BookWithImageFileDTO book)
    {
        await _mediator.Send(new UpdateBookRequest(id, book));
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    [AutoValidation]
    public async Task<ActionResult<BookDTO>> PostBook([FromForm] BookWithImageFileDTO book)
    {
        var response = await _mediator.Send(new AddBookRequest(book));
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _mediator.Send(new DeleteBookRequest(id));
        return Ok();
    }
}

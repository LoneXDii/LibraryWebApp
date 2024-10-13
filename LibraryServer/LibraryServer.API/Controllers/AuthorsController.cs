using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using FluentValidation;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AuthorDTO>> GetAuthors()
    {
        var response = await _mediator.Send(new ListAllAuthorsRequest());
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
    {
        var response = await _mediator.Send(new GetAuthorByIdRequest(id));
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> PutAuthor(int id, AuthorDTO author)
    {
        await _mediator.Send(new UpdateAuthorRequest(id, author));
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO author)
    {
        var response = await _mediator.Send(new AddAuthorRequest(author));
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _mediator.Send(new DeleteAuthorRequest(id));
        return Ok();
    }

    [HttpGet]
    [Route("{id:int}/books")]
    public async Task<ActionResult<List<BookDTO>>> GetAuthorBooks(int id)
    {
        var response = await _mediator.Send(new ListAuthorsBooksRequest(id));
        return Ok(response);
    }
}

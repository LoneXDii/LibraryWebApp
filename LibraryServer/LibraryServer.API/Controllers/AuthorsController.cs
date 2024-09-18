using Microsoft.AspNetCore.Mvc;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Application.DTO;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<ActionResult<AuthorDTO>> GetAuthors()
    {
        return Ok(await _authorService.ListAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
    {
        return Ok(await _authorService.GetByIdAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutGenre(int id, AuthorDTO author)
    {
        await _authorService.UpdateAsync(id, author);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDTO>> PostGenre(AuthorDTO author)
    {
        return Ok(await _authorService.AddAsync(author));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _authorService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet]
    [Route("{id:int}/books")]
    public async Task<ActionResult<List<BookDTO>>> GetAuthorBooks(int id)
    {
        return Ok(await _authorService.ListAuthorsBooksAsync(id));
    }
}

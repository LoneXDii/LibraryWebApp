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
        var response = await _authorService.ListAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
    {
        var response = await _authorService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAuthor(int id, AuthorDTO author)
    {
        await _authorService.UpdateAsync(id, author);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO author)
    {
        var response = await _authorService.AddAsync(author);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await _authorService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet]
    [Route("{id:int}/books")]
    public async Task<ActionResult<List<BookDTO>>> GetAuthorBooks(int id)
    {
        var response = await _authorService.ListAuthorsBooksAsync(id);
        return Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;

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
    public async Task<ActionResult<ResponseData<List<AuthorDTO>>>> GetAuthors()
    {
        return Ok(await _authorService.ListAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseData<AuthorDTO>>> GetAuthor(int id)
    {
        var response = await _authorService.GetByIdAsync(id);
        if (!response.Successfull)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutGenre(int id, AuthorDTO author)
    {
        await _authorService.UpdateAsync(id, author);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseData<AuthorDTO>>> PostGenre(AuthorDTO author)
    {
        var response = await _authorService.AddAsync(author);
        if (!response.Successfull)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _authorService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet]
    [Route("{id:int}/books")]
    public async Task<ActionResult<ResponseData<List<BookDTO>>>> GetAuthorBooks(int id)
    {
        var response = await _authorService.ListAuthorsBooksAsync(id);
        if (!response.Successfull)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}

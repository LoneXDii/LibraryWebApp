using Microsoft.AspNetCore.Mvc;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Application.DTO;
using Microsoft.AspNetCore.Authorization;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreDTO>>> GetGenres()
    {
        var response = await _genreService.ListAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDTO>> GetGenre(int id)
    {
        var response = await _genreService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> PutGenre(int id, GenreDTO genre)
    {
        await _genreService.UpdateAsync(id, genre);
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genre)
    {
        var response = await _genreService.AddAsync(genre);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _genreService.DeleteAsync(id);
        return Ok();
    }
}

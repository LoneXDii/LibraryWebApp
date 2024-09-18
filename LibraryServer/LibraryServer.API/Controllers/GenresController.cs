using Microsoft.AspNetCore.Mvc;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Application.DTO;

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
        return Ok(await _genreService.ListAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDTO>> GetGenre(int id)
    {
        return Ok(await _genreService.GetByIdAsync(id));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutGenre(int id, GenreDTO genre)
    {
        await _genreService.UpdateAsync(id, genre);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genre)
    {
        return Ok(await _genreService.AddAsync(genre));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _genreService.DeleteAsync(id);
        return Ok();
    }
}

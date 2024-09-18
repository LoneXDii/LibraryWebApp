using Microsoft.AspNetCore.Mvc;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;

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
    public async Task<ActionResult<ResponseData<List<GenreDTO>>>> GetGenres()
    {
        return Ok(await _genreService.ListAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseData<GenreDTO>>> GetGenre(int id)
    {
        var response = await _genreService.GetByIdAsync(id);
        if (!response.Successfull)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutGenre(int id, GenreDTO genre)
    {
        await _genreService.UpdateAsync(id, genre);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseData<GenreDTO>>> PostGenre(GenreDTO genre)
    {
        var response = await _genreService.AddAsync(genre);
        if (!response.Successfull)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _genreService.DeleteAsync(id);
        return Ok();
    }
}

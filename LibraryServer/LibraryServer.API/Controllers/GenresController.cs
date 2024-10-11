using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreDTO>>> GetGenres()
    {
        var response = await _mediator.Send(new ListAllGenresRequest());
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDTO>> GetGenre(int id)
    {
        var response = await _mediator.Send(new GetGenreByIdRequest(id));
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> PutGenre(int id, GenreDTO genre)
    {
        await _mediator.Send(new UpdateGenreRequest(id, genre));
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "admin")]
    public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genre)
    {
        var response = await _mediator.Send(new AddGenreRequest(genre));
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "admin")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await _mediator.Send(new DeleteGenreRequest(id));
        return Ok();
    }
}

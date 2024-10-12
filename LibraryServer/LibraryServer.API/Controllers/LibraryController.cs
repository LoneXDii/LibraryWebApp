using LibraryServer.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserValidationService _userValidationService;

    public LibraryController(IMediator mediator, IUserValidationService userValidationService)
    {
        _mediator = mediator;
        _userValidationService = userValidationService;
    }

    [HttpPost]
    [Authorize]
    [Route("take")]
    public async Task<IActionResult> GiveBook(GiveOrTakeBookDTO giveBook)
    {
        _userValidationService.ValidateUser(giveBook.UserId);
        await _mediator.Send(new GiveBookToUserRequest(giveBook));
        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("user-books/{id}")]
    public async Task<ActionResult<List<TakenBookDTO>>> GetUserBooks(string id)
    {
        _userValidationService.ValidateUser(id);
        var response = await _mediator.Send(new ListUsersTakenBooksRequest(id));
        return Ok(response);
    }
}

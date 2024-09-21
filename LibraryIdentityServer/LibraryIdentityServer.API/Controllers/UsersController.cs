using LibraryIdentityServer.Application.Models;
using LibraryIdentityServer.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIdentityServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        await _userService.CreateUserAsync(model);
        return Ok();
    }
}

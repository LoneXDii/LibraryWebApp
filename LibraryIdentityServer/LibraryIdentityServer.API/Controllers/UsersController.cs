using LibraryIdentityServer.Application.Models;
using LibraryIdentityServer.Application.UseCases.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace LibraryIdentityServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ICreateUserUseCase _createUserUseCase;

    public UsersController(ICreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        await _createUserUseCase.ExecuteAsync(model);
        return Ok();
    }
}

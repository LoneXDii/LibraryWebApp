using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebugController : ControllerBase
{
    [HttpGet]
    [Authorize]
    [Route("user")]
    public IActionResult TestUserEndpoint()
    {
        return Ok();
    }

    [HttpGet]
    [Authorize(Policy = "admin")]
    [Route("admin")]
    public IActionResult TestAdminEndpoint()
    {
        return Ok();
    }
}



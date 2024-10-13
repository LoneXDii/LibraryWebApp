using LibraryServer.API.Services.Interfaces;
using LibraryServer.Application.Exceptions;

namespace LibraryServer.API.Services;

internal class UserValidationService : IUserValidationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpContext _context;

    public UserValidationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = _httpContextAccessor.HttpContext;
    }

    public void ValidateUser(string userId)
    {
        var tokenUserId = _context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var role = _context.User.FindFirst("role")?.Value;
        if (role == "admin")
        {
            return;
        }
        if (tokenUserId != userId)
        {
            throw new ForbiddenException("Access denied");
        }
    }
}

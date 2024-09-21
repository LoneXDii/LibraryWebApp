using LibraryIdentityServer.Application.Models;

namespace LibraryIdentityServer.Application.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(RegisterModel user);
}

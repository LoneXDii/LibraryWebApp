using LibraryIdentityServer.Application.Models;

namespace LibraryIdentityServer.Application.UseCases.CreateUser;

public interface ICreateUserUseCase
{
    Task ExecuteAsync(RegisterModel userModel);
}

using AutoMapper;
using IdentityModel;
using LibraryIdentityServer.Application.Models;
using LibraryIdentityServer.Domain.Common.Models;
using LibraryIdentityServer.Domain.IdentityConfiguration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibraryIdentityServer.Application.UseCases.CreateUser;

internal class CreateUserUseCase : ICreateUserUseCase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public CreateUserUseCase(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(RegisterModel userModel)
    {
        var user = _mapper.Map<AppUser>(userModel);

        await _userManager.CreateAsync(user, userModel.Password);
        await _userManager.AddToRoleAsync(user, Config.Customer);
        await _userManager.AddClaimsAsync(user, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, user.Name),
            new Claim(JwtClaimTypes.Role, Config.Customer)
        });
    }
}

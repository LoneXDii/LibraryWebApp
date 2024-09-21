using IdentityModel;
using LibraryIdentityServer.Application.IdentityConfiguration;
using LibraryIdentityServer.Application.Models;
using LibraryIdentityServer.Application.Services.Interfaces;
using LibraryIdentityServer.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibraryIdentityServer.Application.Services;

internal class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task CreateUserAsync(RegisterModel userModel)
    {
        AppUser user = new()
        {
            UserName = userModel.Email,
            Email = userModel.Email,
            EmailConfirmed = true,
            PhoneNumber = userModel.Phone,
            Name = userModel.Name,
            Surname = userModel.Surname
        };

        await _userManager.CreateAsync(user, userModel.Password);
        await _userManager.AddToRoleAsync(user, Config.Customer);
        await _userManager.AddClaimsAsync(user, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, user.Name),
            new Claim(JwtClaimTypes.Role, Config.Customer)
        });
    }
}

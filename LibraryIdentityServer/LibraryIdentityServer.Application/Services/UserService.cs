using FluentValidation;
using IdentityModel;
using LibraryIdentityServer.Application.IdentityConfiguration;
using LibraryIdentityServer.Application.Models;
using LibraryIdentityServer.Application.Services.Interfaces;
using LibraryIdentityServer.Application.Validators;
using LibraryIdentityServer.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;

namespace LibraryIdentityServer.Application.Services;

internal class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserValidator _validator;

    public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _validator = new UserValidator();
    }

    public async Task CreateUserAsync(RegisterModel userModel)
    {
        Validate(userModel);

        AppUser user = new()
        {
            UserName = userModel.Email,
            Email = userModel.Email,
            EmailConfirmed = true,
            PhoneNumber = userModel.Phone,
            Name = $"{userModel.Name} {userModel.Surname}",
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

    private void Validate(RegisterModel model)
    {
        var validationResult = _validator.Validate(model);

        if (!validationResult.IsValid)
        {
            var sb = new StringBuilder();
            foreach (var failure in validationResult.Errors)
            {
                sb.AppendLine(failure.ErrorMessage);
            }
            throw new ValidationException(sb.ToString());
        }
    }
}

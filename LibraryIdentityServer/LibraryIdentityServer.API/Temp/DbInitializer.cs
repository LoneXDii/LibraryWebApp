using LibraryIdentityServer.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;
using LibraryIdentityServer.Application.IdentityConfiguration;


namespace LibraryIdentityServer.API.Temp;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedData()
    {
        if (await _roleManager.FindByNameAsync(Config.Admin) is null)
        {
            await _roleManager.CreateAsync(new IdentityRole(Config.Admin));
            await _roleManager.CreateAsync(new IdentityRole(Config.Customer));
        }
        else
        {
            return;
        }

        AppUser adminUser = new()
        {
            UserName = "admin1@gmail.com",
            Email = "admin1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "11111111111",
            Name = "Test Admin"
        };
        await _userManager.CreateAsync(adminUser, "Admin123*");
        await _userManager.AddToRoleAsync(adminUser, Config.Admin);

        var claims1 = await _userManager.AddClaimsAsync(adminUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.Name),
            new Claim(JwtClaimTypes.Role, Config.Admin)
        });

        AppUser customerUser = new()
        {
            UserName = "customer1@gmail.com",
            Email = "customer1@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "11111111111",
            Name = "Test Customer"
        };
        await _userManager.CreateAsync(customerUser, "Client123*");
        await _userManager.AddToRoleAsync(customerUser, Config.Customer);

        var claims2 = await _userManager.AddClaimsAsync(customerUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, customerUser.Name),
            new Claim(JwtClaimTypes.Role, Config.Customer)
        });
    }
}

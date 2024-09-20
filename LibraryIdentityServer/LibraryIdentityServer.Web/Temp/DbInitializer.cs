using LibraryIdentityServer.Web.IdentityData;
using LibraryIdentityServer.Web.Data;
using LibraryIdentityServer.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;

namespace LibraryIdentityServer.Web.Temp;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(AppDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedData()
    {
        if (await _roleManager.FindByNameAsync(StaticData.Admin) is null)
        {
            await _roleManager.CreateAsync(new IdentityRole(StaticData.Admin));
            await _roleManager.CreateAsync(new IdentityRole(StaticData.Customer));
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
        await _userManager.AddToRoleAsync(adminUser, StaticData.Admin);

        var claims1 = await _userManager.AddClaimsAsync(adminUser, new Claim[] 
        {
            new Claim(JwtClaimTypes.Name, adminUser.Name),
            new Claim(JwtClaimTypes.Role, StaticData.Admin)
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
        await _userManager.AddToRoleAsync(customerUser, StaticData.Customer);

        var claims2 = await _userManager.AddClaimsAsync(customerUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.Name),
            new Claim(JwtClaimTypes.Role, StaticData.Customer)
        });
    }
}

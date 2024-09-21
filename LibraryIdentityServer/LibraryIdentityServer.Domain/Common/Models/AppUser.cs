using Microsoft.AspNetCore.Identity;

namespace LibraryIdentityServer.Domain.Common.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; } = "";
}

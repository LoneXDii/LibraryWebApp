using Microsoft.AspNetCore.Identity;

namespace LibraryIdentityServer.Web.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
}

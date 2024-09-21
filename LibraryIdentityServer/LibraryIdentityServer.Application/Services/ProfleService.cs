using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using LibraryIdentityServer.Domain.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LibraryIdentityServer.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
    private readonly UserManager<AppUser> _userMgr;

    public ProfileService(UserManager<AppUser> userMgr, 
                          IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory)
    {
        _userMgr = userMgr;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        string sub = context.Subject.GetSubjectId();
        AppUser user = await _userMgr.FindByIdAsync(sub);

        ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

        List<Claim> claims = userClaims.Claims.ToList();
        claims = claims.Where(u => context.RequestedClaimTypes
                       .Contains(u.Type))
                       .ToList();

        claims.Add(new Claim(JwtClaimTypes.Name, user.Name));
        claims.Add(new Claim(JwtClaimTypes.Id, user.Id));
        if (_userMgr.SupportsUserRole)
        {
            IList<string> roles = await _userMgr.GetRolesAsync(user);
            foreach (var rolename in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, rolename));
            }
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string sub = context.Subject.GetSubjectId();
        AppUser user = await _userMgr.FindByIdAsync(sub);
        context.IsActive = user != null;
    }
}

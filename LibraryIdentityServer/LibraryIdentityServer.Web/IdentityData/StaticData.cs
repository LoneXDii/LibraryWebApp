using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace LibraryIdentityServer.Web.IdentityData;

public static class StaticData
{
    public const string Admin = "admin";
    public const string Customer = "customer";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource> 
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("library", "Library Server"),
        };
    

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "library",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { 
                    "library",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email
                }
                //add client uris
            }
        };
}

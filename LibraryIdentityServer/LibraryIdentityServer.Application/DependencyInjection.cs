using Duende.IdentityServer.Services;
using LibraryIdentityServer.Application.IdentityConfiguration;
using LibraryIdentityServer.Application.Services;
using LibraryIdentityServer.DataAcess;
using LibraryIdentityServer.Domain.Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryIdentityServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string? connStr)
    {
        services.AddDataAcess(connStr)
                .AddIdentityServer(opt =>
                {
                    opt.Events.RaiseErrorEvents = true;
                    opt.Events.RaiseInformationEvents = true;
                    opt.Events.RaiseFailureEvents = true;
                    opt.Events.RaiseSuccessEvents = true;
                    opt.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

        services.AddScoped<IProfileService, ProfileService>();

        return services;
    }
}



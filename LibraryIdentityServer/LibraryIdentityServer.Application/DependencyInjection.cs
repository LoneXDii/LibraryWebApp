using Duende.IdentityServer.Services;
using FluentValidation;
using LibraryIdentityServer.Application.Mapping;
using LibraryIdentityServer.Application.Services;
using LibraryIdentityServer.Application.UseCases.CreateUser;
using LibraryIdentityServer.DataAcess;
using LibraryIdentityServer.Domain.Common.Models;
using LibraryIdentityServer.Domain.IdentityConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace LibraryIdentityServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string? connStr, IConfiguration cfg)
    {
        services.AddDataAcess(connStr)
                .AddIdentityServer(opt =>
                {
                    opt.Events.RaiseErrorEvents = true;
                    opt.Events.RaiseInformationEvents = true;
                    opt.Events.RaiseFailureEvents = true;
                    opt.Events.RaiseSuccessEvents = true;
                    opt.EmitStaticAudienceClaim = true;
                    opt.IssuerUri = cfg["ISSUER_BASE"] ?? "https://localhost:7002";
                })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

        services.AddScoped<IProfileService, ProfileService>()
                .AddScoped<ICreateUserUseCase, CreateUserUseCase>()
                .AddAutoMapper(typeof(AppMappingProfile))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();

        return services;
    }
}



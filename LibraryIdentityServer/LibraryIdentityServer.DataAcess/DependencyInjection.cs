﻿using LibraryIdentityServer.DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using LibraryIdentityServer.Domain.Common.Models;

namespace LibraryIdentityServer.DataAcess;


public static class DependencyInjection
{
    public static IServiceCollection AddDataAcess(this IServiceCollection services, string? connStr)
    {
        services.AddDbContext<AppDbContext>(opt =>
                    opt.UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 36)),
                                 opt => opt.EnableRetryOnFailure()),
                    ServiceLifetime.Scoped);

        services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        return services;
    }
}

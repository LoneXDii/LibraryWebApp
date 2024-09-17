﻿using LibraryServer.DataAccess.Data;
using LibraryServer.DataAccess.Repositories;
using LibraryServer.DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServer.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAcess(this IServiceCollection services, string connStr)
    {
        services.AddDbContext<AppDbContext>(opt =>
                    opt.UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 36))),
                    ServiceLifetime.Scoped)
                .AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

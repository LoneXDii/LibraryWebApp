using AutoMapper.Extensions.ExpressionMapping;
using LibraryServer.Application.Mapping;
using LibraryServer.Application.Services;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string? connStr, IConfiguration cfg)
    {
        services.AddDataAcess(connStr, cfg)
                .AddAutoMapper(cfg =>
                    {
                        cfg.AddExpressionMapping();
                    }, typeof(AppMappingProfile))
                .AddScoped<IBookService, BookService>()
                .AddScoped<IAuthorService, AuthorService>()
                .AddScoped<IGenreService, GenreService>()
                .AddScoped<IUserValidationService, UserValidationService>();

        return services;
    }
}

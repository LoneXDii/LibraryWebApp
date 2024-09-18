using LibraryServer.Application.Mapping;
using LibraryServer.Application.Services;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string? connStr)
    {
        services.AddDataAcess(connStr)
                .AddAutoMapper(typeof(AppMappingProfile))
                .AddScoped<IBookService, BookService>()
                .AddScoped<IAuthorService, AuthorService>()
                .AddScoped<IGenreService, GenreService>();

        return services;
    }
}

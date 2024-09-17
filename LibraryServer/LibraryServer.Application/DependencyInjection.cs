using LibraryServer.Application.Mapping;
using LibraryServer.Application.Services;
using LibraryServer.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AppMappingProfile))
                .AddScoped<IBookService, BookService>();

        return services;
    }
}

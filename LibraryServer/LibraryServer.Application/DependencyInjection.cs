using LibraryServer.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AppMappingProfile));

        return services;
    }
}

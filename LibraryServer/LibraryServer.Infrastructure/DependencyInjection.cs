using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using LibraryServer.Infrastructure.BlobService;

namespace LibraryServer.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAcess(this IServiceCollection services, string? connStr, IConfiguration cfg)
    {
        services.AddDbContext<AppDbContext>(opt =>
                    opt.UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 36)),
                                 opt => opt.EnableRetryOnFailure()),
                    ServiceLifetime.Scoped)
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<IUnitOfWork, UnitOfWork>();

        var azuriteConnStr = cfg["AZURE_CONNECTION"] ?? cfg.GetConnectionString("AZURE_CONNECTION");
        services.AddSingleton<IBlobService, BlobService>()
                .AddSingleton(_ => new BlobServiceClient(azuriteConnStr));

        return services;
    }
}

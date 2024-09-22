using AutoMapper.Extensions.ExpressionMapping;
using Azure.Storage.Blobs;
using LibraryServer.Application.Mapping;
using LibraryServer.Application.Services;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Application.Services.StorageServices;
using LibraryServer.Application.Services.StorageServices.Interfaces;
using LibraryServer.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, string? connStr, IConfiguration cfg)
    {
        services.AddDataAcess(connStr)
                .AddAutoMapper(cfg =>
                    {
                        cfg.AddExpressionMapping();
                    }, typeof(AppMappingProfile))
                .AddScoped<IBookService, BookService>()
                .AddScoped<IAuthorService, AuthorService>()
                .AddScoped<IGenreService, GenreService>()
                .AddScoped<IUserValidationService, UserValidationService>();


        var azuriteConnStr = cfg["AZURE_CONNECTION"] ?? cfg.GetConnectionString("AZURE_CONNECTION");
        services.AddSingleton<IBlobService, BlobService>()
                .AddSingleton(_ => new BlobServiceClient(azuriteConnStr));

        return services;
    }
}

using AutoMapper.Extensions.ExpressionMapping;
using LibraryServer.Application.Mapping;
using LibraryServer.Application.Services;
using LibraryServer.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using LibraryServer.Application.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;

namespace LibraryServer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
                    {
                        cfg.AddExpressionMapping();
                    }, typeof(AppMappingProfile))
                .AddScoped<IUserValidationService, UserValidationService>()
                .AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();


        return services;
    }
}

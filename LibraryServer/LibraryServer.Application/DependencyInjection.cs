using AutoMapper.Extensions.ExpressionMapping;
using LibraryServer.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
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
                .AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation(cfg => 
                {
                    cfg.EnableFormBindingSourceAutomaticValidation = true;
                    cfg.EnableBodyBindingSourceAutomaticValidation = true;
                });

        return services;
    }
}

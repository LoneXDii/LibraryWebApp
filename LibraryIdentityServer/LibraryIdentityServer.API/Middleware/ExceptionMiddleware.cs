﻿using FluentValidation;
using System.Text.Json;
using LibraryIdentityServer.Domain.Common.Exceptions;

namespace LibraryIdentityServer.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var error = new ApiException
        {
            Message = ex.Message,
            Details = ex.StackTrace,
            StatusCode = ex switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            }
        };

        _logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = error.StatusCode;
        await context.Response.WriteAsync(
                            JsonSerializer.Serialize(error,
                                                     new JsonSerializerOptions
                                                     {
                                                         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                                                     }));
    }
}

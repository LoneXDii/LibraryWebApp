using LibraryIdentityServer.API.Middleware;
using LibraryIdentityServer.Application;
using LibraryIdentityServer.Domain.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
var user = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
var usersDataBase = builder.Configuration["MYSQL_DATABASE2"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");

var connStr = $"server={host};user={user};password={password};port={port};database={usersDataBase}";

builder.Services.AddApplication(connStr, builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddHealthChecks();

var client_uri = builder.Configuration["CLIENT_URI"] ?? "http://localhost:3000";
builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy",
    builder => builder.WithOrigins(client_uri)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CorsPolicy");
app.UseIdentityServer();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

await SeedDatabaseAsync();

app.MapHealthChecks("/health");
app.MapControllers();

app.UseAuthorization();

app.Run();


async Task SeedDatabaseAsync()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await DbInitializer.SeedData();
    }
}
using Duende.IdentityServer.Services;
using LibraryIdentityServer.API.Temp;
using LibraryIdentityServer.Application;
using LibraryIdentityServer.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connStr = builder.Configuration.GetConnectionString("MySQLConnection");

builder.Services.AddApplication(connStr);

builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseIdentityServer();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await SeedDatabase();

app.MapControllers();

app.UseAuthorization();

app.Run();


async Task SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await DbInitializer.SeedData();
    }
}
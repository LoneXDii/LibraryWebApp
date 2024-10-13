using LibraryServer.API.Middleware;
using LibraryServer.Application;
using LibraryServer.Domain;
using LibraryServer.Domain.Abstactions.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
var user = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
var usersDataBase = builder.Configuration["MYSQL_DATABASE"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");

var connStr = $"server={host};user={user};password={password};port={port};database={usersDataBase}";

builder.Services.AddInfrastructure(connStr, builder.Configuration)
                .AddApplication();

var key = builder.Configuration.GetValue<string>("Kestrel:Secret");

var identityBase = builder.Configuration["IDENTITY_BASE"] ?? "https://localhost:7002";
builder.Services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.Authority = identityBase;
                    opt.RequireHttpsMetadata = false;
                    opt.Audience = $"{identityBase}/resources";
                });

builder.Services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("admin", p => p.RequireRole("admin"));
                });


var client_uri = builder.Configuration["CLIENT_URI"] ?? "http://localhost:3000";
builder.Services.AddCors(opt => opt.AddPolicy("CorsPolicy",
    builder => builder.WithOrigins(client_uri)
                      .AllowAnyMethod()
                      .AllowAnyHeader()));

builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
    await seeder.SeedDataAsync();
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();

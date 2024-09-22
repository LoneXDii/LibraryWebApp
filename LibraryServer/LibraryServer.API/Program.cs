using LibraryServer.API.Middleware;
using LibraryServer.API.Temp;
using LibraryServer.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();


//TEMP
//HttpClientHandler handler = new HttpClientHandler();
//handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
//var client = new HttpClient(handler);
var client = new HttpClient();
var a = await client.GetAsync("http://identity_api:7002/.well-known/openid-configuration");
Console.WriteLine($"------------->{a.StatusCode}");
//TEMP END


var host = builder.Configuration["DBHOST"] ?? "localhost";
var port = builder.Configuration["DBPORT"] ?? "3306";
var password = builder.Configuration["MYSQL_PASSWORD"] ?? builder.Configuration.GetConnectionString("MYSQL_PASSWORD");
var user = builder.Configuration["MYSQL_USER"] ?? builder.Configuration.GetConnectionString("MYSQL_USER");
var usersDataBase = builder.Configuration["MYSQL_DATABASE"] ?? builder.Configuration.GetConnectionString("MYSQL_DATABASE");

var connStr = $"server={host};user={user};password={password};port={port};database={usersDataBase}";
builder.Services.AddApplication(connStr);

var key = builder.Configuration.GetValue<string>("Kestrel:Secret");

builder.Services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    //opt.Authority = "https://localhost:7002";
                    opt.Authority = "http://identity_api:7002";
                    opt.RequireHttpsMetadata = false;
                    //opt.Audience = "http://localhost:7002/resources";
                    opt.Audience = "http://identity_api:7002/resources";
                });

builder.Services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("admin", p => p.RequireRole("admin"));
                });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
await DbInitializer.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

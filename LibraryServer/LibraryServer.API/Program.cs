using LibraryServer.API.Middleware;
using LibraryServer.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStr = builder.Configuration.GetConnectionString("MySQLConnection");
builder.Services.AddApplication(connStr);

var key = builder.Configuration.GetValue<string>("Kestrel:Secret");
builder.Services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.Authority = "https://localhost:7002";
                    opt.RequireHttpsMetadata = true;
                    opt.Audience = "https://localhost:7002/resources";
                });

builder.Services.AddAuthorization(opt =>
                {
                    opt.AddPolicy("admin", p => p.RequireRole("admin"));
                });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
//await DbInitializer.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

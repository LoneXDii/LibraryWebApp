using Duende.IdentityServer.Services;
using LibraryIdentityServer.Web.Data;
using LibraryIdentityServer.Web.IdentityData;
using LibraryIdentityServer.Web.Models;
using LibraryIdentityServer.Web.Services;
using LibraryIdentityServer.Web.Temp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connStr = builder.Configuration.GetConnectionString("MySQLConnection");
builder.Services.AddDbContext<AppDbContext>(opt =>
                   opt.UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 36)),
                                opt => opt.EnableRetryOnFailure()),
                   ServiceLifetime.Scoped);

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer(opt =>
                {
                    opt.Events.RaiseErrorEvents = true;
                    opt.Events.RaiseInformationEvents = true;
                    opt.Events.RaiseFailureEvents = true;
                    opt.Events.RaiseSuccessEvents = true;
                    opt.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(StaticData.IdentityResources)
                .AddInMemoryApiScopes(StaticData.ApiScopes)
                .AddInMemoryClients(StaticData.Clients)
                .AddAspNetIdentity<AppUser>()
                .AddDeveloperSigningCredential()
                .AddProfileService<ProfileService>();

builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseIdentityServer();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

await SeedDatabase();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages()
   .RequireAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


async Task SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await DbInitializer.SeedData();
    }
}
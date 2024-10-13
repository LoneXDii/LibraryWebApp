namespace LibraryServer.Infrastructure.DataSeeder;

internal interface IDbSeeder
{
    Task SeedDataAsync();
}

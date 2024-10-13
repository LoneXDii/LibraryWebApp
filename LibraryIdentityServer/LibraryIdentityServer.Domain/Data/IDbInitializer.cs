namespace LibraryIdentityServer.Domain.Data;

public interface IDbInitializer
{
    Task SeedData();
}

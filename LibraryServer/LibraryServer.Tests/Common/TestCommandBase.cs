using LibraryServer.DataAccess.Data;

namespace LibraryServer.Tests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly AppDbContext _dbContext;

    public TestCommandBase()
    {
        _dbContext = LibraryContextFactory.Create();
    }

    public void Dispose()
    {
        LibraryContextFactory.Destroy(_dbContext);
    }
}

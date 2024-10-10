using LibraryServer.Domain.Common.Exceptions;
using LibraryServer.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Tests.Books.UseCases;

public class DeleteBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task DeleteBookCommand_Success()
    {
        await _bookService.DeleteAsync(1);
        Assert.Null(await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == 1));
    }

    [Fact]
    public async Task DeleteBookCommand_NotFoundError()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _bookService.DeleteAsync(123));
        Assert.Equal("No book with id=123", exception.Message);
    }
}

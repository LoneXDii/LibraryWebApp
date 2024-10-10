using LibraryServer.Application.DTO;
using LibraryServer.DataAccess.Common.Exceptions;
using LibraryServer.DataAccess.Entities;
using LibraryServer.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Tests.Books.UseCases;

public class GetBookByIdCommandTests : TestCommandBase
{
    [Fact]
    public async Task GetBookByIdCommand_Success()
    {
        var book = await _bookService.GetByIdAsync(1);
        Assert.NotNull(book);
    }

    [Fact]
    public async Task GetBookByIdCommand_NotFoundError()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _bookService.GetByIdAsync(123));
    }
}

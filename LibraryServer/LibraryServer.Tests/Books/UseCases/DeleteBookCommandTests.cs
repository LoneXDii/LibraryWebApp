using LibraryServer.Application.Exceptions;
using LibraryServer.Application.UseCases.BookUseCases.Commands;
using LibraryServer.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Tests.Books.UseCases;

public class DeleteBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task DeleteBookCommand_Success()
    {
        //Arrange
        var command = new DeleteBookRequest(1);
        var handler = new DeleteBookRequestHandler(_unitOfWork, _blobService);

        //Act
        await handler.Handle(command);

        //Assert
        Assert.Null(await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == 1));
    }

    [Fact]
    public async Task DeleteBookCommand_NotFoundError()
    {
        //Arrange
        var command = new DeleteBookRequest(123);
        var handler = new DeleteBookRequestHandler(_unitOfWork, _blobService);

        //Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command));

        //Assert
        Assert.Equal("No book with id=123", exception.Message);
    }
}

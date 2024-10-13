using LibraryServer.Application.Exceptions;
using LibraryServer.Application.UseCases.BookUseCases.Queries;
using LibraryServer.Tests.Common;


namespace LibraryServer.Tests.Books.UseCases;

public class GetBookByIdCommandTests : TestCommandBase
{
    [Fact]
    public async Task GetBookByIdCommand_Success()
    {
        //Arrange
        var command = new GetBookByIdRequest(1);
        var handler = new GetBookByIdRequestHandler(_unitOfWork, _mapper);

        //Act
        var book = await handler.Handle(command);

        //Assert
        Assert.NotNull(book);
    }

    [Fact]
    public async Task GetBookByIdCommand_NotFoundError()
    {
        //Arrange
        var command = new GetBookByIdRequest(123);
        var handler = new GetBookByIdRequestHandler(_unitOfWork, _mapper);

        //Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command));

        //Assert
        Assert.Equal("No book with id=123", exception.Message);
    }
}

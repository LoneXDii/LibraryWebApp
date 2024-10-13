using LibraryServer.Tests.Common;
using LibraryServer.Application.DTO;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Xunit.Sdk;
using LibraryServer.Application.UseCases.BookUseCases.Commands;

namespace LibraryServer.Tests.Books.UseCases;

public class CreateBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task CreateBookCommand_Success()
    {
        //Arrange
        var command = new AddBookRequest(new BookWithImageFileDTO
        {
            Book = new BookDTO
            {
                ISBN = "978-1-56619-909-5",
                Title = "Test Book",
                Description = "Test Description",
                GenreId = 1,
                AuthorId = 1,
                Quantity = 11
            }
        });

        var handler = new AddBookRequestHandler(_unitOfWork, _mapper, _blobService, _cfg);

        //Act
        var book = await handler.Handle(command);

        //Assert
        Assert.NotNull(await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id));
    }
}

using FluentValidation;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Exceptions;
using LibraryServer.Application.UseCases.BookUseCases.Commands;
using LibraryServer.Domain.Entities;
using LibraryServer.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Tests.Books.UseCases;

public class UpdateBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task UpdateBookCommand_Success()
    {
        //Arrange
        var book = new BookWithImageFileDTO
        {
            Book = new BookDTO
            {
                ISBN = "978-1-56619-909-9",
                Title = "1111",
                Description = "AaaAAa",
                GenreId = 2,
                AuthorId = 2,
                Quantity = 2,
                Image = "Some img"
            }
        };

        var command = new UpdateBookRequest(1, book);
        var handler = new UpdateBookRequestHandler(_unitOfWork, _mapper, _blobService, _cfg);

        //Act
        await handler.Handle(command);

        //Assert
        var expBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == 1);

        Assert.NotNull(expBook);
        Assert.Equal(expBook.Title, book.Book.Title);
        Assert.Equal(expBook.ISBN, book.Book.ISBN);
        Assert.Equal(expBook.Image, book.Book.Image);
        Assert.Equal(expBook.Description, book.Book.Description);
        Assert.Equal(expBook.GenreId, book.Book.GenreId);
        Assert.Equal(expBook.AuthorId, book.Book.AuthorId);
        Assert.Equal(expBook.Quantity, book.Book.Quantity);
    }

    [Fact]
    public async Task UpdateBookCommandTest_NotFoundError()
    {
        //Arrange
        var book = new BookWithImageFileDTO();
        var command = new UpdateBookRequest(123, book);
        var handler = new UpdateBookRequestHandler(_unitOfWork, _mapper, _blobService, _cfg);

        //Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command));

        //Assert
        Assert.Equal("No book with id=123", exception.Message);
    }
}

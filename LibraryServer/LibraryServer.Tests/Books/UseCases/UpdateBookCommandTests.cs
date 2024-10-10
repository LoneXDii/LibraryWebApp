using FluentValidation;
using LibraryServer.Application.DTO;
using LibraryServer.DataAccess.Common.Exceptions;
using LibraryServer.DataAccess.Entities;
using LibraryServer.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Tests.Books.UseCases;

public class UpdateBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task UpdateBookCommand_Success()
    {
        var book = new BookDTO
        {
            ISBN = "978-1-56619-909-9",
            Title = "1111",
            Description = "AaaAAa",
            GenreId = 2,
            AuthorId = 2,
            Quantity = 2,
            Image = "Some img"
        };

        await _bookService.UpdateAsync(1, book);
        var expBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == 1);

        Assert.NotNull(expBook);
        Assert.Equal(expBook.Title, book.Title);
        Assert.Equal(expBook.ISBN, book.ISBN);
        Assert.Equal(expBook.Image, book.Image);
        Assert.Equal(expBook.Description, book.Description);
        Assert.Equal(expBook.GenreId, book.GenreId);
        Assert.Equal(expBook.AuthorId, book.AuthorId);
        Assert.Equal(expBook.Quantity, book.Quantity);
    }

    [Fact]
    public async Task UpdateBookCommandTest_NotFoundError()
    {
        var book = new BookDTO();
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _bookService.UpdateAsync(123, book));
    }

    [Fact]
    public async Task UpdateBookCommandTest_ValidationError()
    {
        var book = new BookDTO
        {
            ISBN = "123",
            Title = "1111",
            Description = "AaaAAa",
            GenreId = 2,
            AuthorId = 2,
            Quantity = 2,
            Image = "Some img"
        };

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookService.UpdateAsync(1, book));
    }
}

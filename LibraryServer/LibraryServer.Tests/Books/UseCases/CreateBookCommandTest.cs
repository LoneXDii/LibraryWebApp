using LibraryServer.Tests.Common;
using LibraryServer.Application.DTO;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace LibraryServer.Tests.Books.UseCases;

public class CreateBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task CreateBookCommand_Success()
    {
        //Act
        var book = await _bookService.AddAsync(new BookDTO 
            {
                ISBN = "978-1-56619-911-7",
                Title = "Test Book",
                Description = "Test Description",
                GenreId = 1,
                AuthorId = 1,
                Quantity = 11
            });

        //Assert
        Assert.NotNull(await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id));
    }

    [Fact]
    public async Task CreateBookCommand_ValidationError()
    {
        var book = new BookDTO
        {
            ISBN = "AAA",
            Title = "Test Book",
            Description = "Test Description",
            GenreId = 1,
            AuthorId = 1,
            Quantity = 11
        };

        var exception = await Assert.ThrowsAsync<ValidationException>(() => _bookService.AddAsync(book));
    }
}

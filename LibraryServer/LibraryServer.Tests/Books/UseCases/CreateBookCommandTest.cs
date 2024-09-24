using AutoMapper;
using LibraryServer.DataAccess.Repositories;
using LibraryServer.Domain.Entities;
using LibraryServer.Tests.Common;
using LibraryServer.Application.Mapping;
using AutoMapper.Extensions.ExpressionMapping;
using LibraryServer.Application.Services; 
using LibraryServer.Application.DTO;
using Microsoft.EntityFrameworkCore;

namespace LibraryServer.Tests.Books.UseCases;

public class CreateBookCommandTests : TestCommandBase
{
    [Fact]
    public async Task CreateBookCommand_Success()
    {
        //Arrange
        var authorRepository = new Repository<Author>(_dbContext);
        var genreRepository = new Repository<Genre>(_dbContext);
        var bookRepository = new Repository<Book>(_dbContext);
        var takenBookRepository = new Repository<TakenBook>(_dbContext);
        var unitOfWork = new UnitOfWork(_dbContext, bookRepository, authorRepository, genreRepository, takenBookRepository);

        var mapperCfg = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AppMappingProfile());
            cfg.AddExpressionMapping();
        });
        var mapper = mapperCfg.CreateMapper();
        var bookService = new BookService(unitOfWork, mapper);

        //Act
        var book = await bookService.AddAsync(new BookDTO 
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
}

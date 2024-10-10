using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LibraryServer.Application.Mapping;
using LibraryServer.Application.Services;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Data;
using LibraryServer.DataAccess.Abstactions;
using LibraryServer.DataAccess.Entities;

namespace LibraryServer.Tests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly AppDbContext _dbContext;
    protected readonly IBookService _bookService;
    protected readonly IMapper _mapper;

    public TestCommandBase()
    {
        _dbContext = LibraryContextFactory.Create();
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
        _mapper = mapperCfg.CreateMapper();
        _bookService = new BookService(unitOfWork, _mapper);
    }

    public void Dispose()
    {
        LibraryContextFactory.Destroy(_dbContext);
    }
}

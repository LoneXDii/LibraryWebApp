using LibraryServer.DataAccess.Data;
using LibraryServer.DataAccess.Repositories.Interfaces;
using LibraryServer.Domain.Entities;

namespace LibraryServer.DataAccess.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext, IRepository<Book> bookRepository, 
                      IRepository<Author> authorRepository, IRepository<Genre> genreRepository, 
                      IRepository<TakenBook> takenBookRepository)
    {
        _dbContext = dbContext;
        BookRepository = bookRepository;
        AuthorRepository = authorRepository;
        GenreRepository = genreRepository;
        TakenBookRepository = takenBookRepository;
    }

    public IRepository<Book> BookRepository { get; private set; }
    public IRepository<Author> AuthorRepository { get; private set; }
    public IRepository<Genre> GenreRepository { get; private set; }
    public IRepository<TakenBook> TakenBookRepository { get; private set; }

    public async Task CreateDataBaseAsync() => await _dbContext.Database.EnsureCreatedAsync();
    public async Task DeleteDataBaseAsync() => await _dbContext.Database.EnsureDeletedAsync();
    public async Task SaveAllAsync() => await _dbContext.SaveChangesAsync();
}
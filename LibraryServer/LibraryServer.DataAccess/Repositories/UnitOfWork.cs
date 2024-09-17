using LibraryServer.DataAccess.Data;
using LibraryServer.DataAccess.Repositories.Interfaces;
using LibraryServer.Domain.Entities;

namespace LibraryServer.DataAccess.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly Lazy<IRepository<Book>> _bookRepository;
    private readonly Lazy<IRepository<Author>> _authorRepository;
    private readonly Lazy<IRepository<Genre>> _genreRepository;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _bookRepository = new Lazy<IRepository<Book>>(() => new Repository<Book>(_dbContext));
        _authorRepository = new Lazy<IRepository<Author>>(() => new Repository<Author>(_dbContext));
        _genreRepository = new Lazy<IRepository<Genre>>(() => new Repository<Genre>(_dbContext));
    }

    public IRepository<Book> BookRepository => _bookRepository.Value;
    public IRepository<Author> AuthorRepository => _authorRepository.Value;
    public IRepository<Genre> GenreRepository => _genreRepository.Value;

    public async Task CreateDataBaseAsync() => await _dbContext.Database.EnsureCreatedAsync();
    public async Task DeleteDataBaseAsync() => await _dbContext.Database.EnsureDeletedAsync();
    public async Task SaveAllAsync() => await _dbContext.SaveChangesAsync();
}

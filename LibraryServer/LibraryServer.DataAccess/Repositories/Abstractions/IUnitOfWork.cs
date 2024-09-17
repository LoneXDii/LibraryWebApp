using LibraryServer.Domain.Entities;

namespace LibraryServer.DataAccess.Repositories.Abstractions;

public interface IUnitOfWork
{
    IRepository<Book> BookRepository { get; }
    IRepository<Author> AuthorRepository { get; }
    IRepository<Genre> GenreRepository { get; }
    public Task SaveAllAsync();
    public Task DeleteDataBaseAsync();
    public Task CreateDataBaseAsync();
}

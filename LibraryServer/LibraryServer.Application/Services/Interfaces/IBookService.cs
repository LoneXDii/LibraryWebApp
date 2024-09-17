using LibraryServer.Application.DTO;
using System.Linq.Expressions;

namespace LibraryServer.Application.Services.Interfaces;

public interface IBookService
{
    Task<IReadOnlyList<BookDTO>> ListAsync(Expression<Func<BookDTO, bool>>? filter, int pageNo = 1, int pageSize = 9);
    Task<BookDTO> GetByIdAsync(int id);
    Task<BookDTO?> FirstOrDefaultAsync(Expression<Func<BookDTO, bool>> filter);
    Task AddAsync(BookDTO book);
    Task UpdateAsync(int id, BookDTO book);
    Task DeleteAsync(BookDTO book);

    //Give to user and add image methods
}

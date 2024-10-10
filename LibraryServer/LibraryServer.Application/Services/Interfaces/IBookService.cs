using LibraryServer.Application.DTO;
using LibraryServer.DataAccess.Common.Models;
using System.Linq.Expressions;

namespace LibraryServer.Application.Services.Interfaces;

public interface IBookService
{
    Task<PaginatedListModel<BookDTO>> ListAsync(string? genreName, int pageNo = 1, int pageSize = 9);
    Task<BookDTO> GetByIdAsync(int id);
    Task<BookDTO> FirstOrDefaultAsync(Expression<Func<BookDTO, bool>> filter);
    Task<BookDTO> AddAsync(BookDTO book);
    Task UpdateAsync(int id, BookDTO book);
    Task DeleteAsync(int id);
    Task GiveToUserAsync(int id, string userId);
    Task TakeFromUserAsync(int id, string userId);
    Task<List<TakenBookDTO>> GetUserBooksAsync(string userId);
    //add image method
}

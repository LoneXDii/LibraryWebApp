using LibraryServer.Application.DTO;
using LibraryServer.Domain.Common.Models;
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
    Task GiveToUser(int id, string userId);
    Task TakeFromUser(int id, string userId);
    Task<List<TakenBookDTO>> GetUserBooks(string userId);
    //add image method
}

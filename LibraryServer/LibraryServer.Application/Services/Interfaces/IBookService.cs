using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
using System.Linq.Expressions;

namespace LibraryServer.Application.Services.Interfaces;

public interface IBookService
{
    Task<ResponseData<DataListModel<BookDTO>>> ListAsync(Expression<Func<BookDTO, bool>>? filter, int pageNo = 1, int pageSize = 9);
    Task<ResponseData<BookDTO>> GetByIdAsync(int id);
    Task<ResponseData<BookDTO>> FirstOrDefaultAsync(Expression<Func<BookDTO, bool>> filter);
    Task<ResponseData<BookDTO>> AddAsync(BookDTO book);
    Task UpdateAsync(int id, BookDTO book);
    Task DeleteAsync(int id);

    //Give to user and add image methods
}

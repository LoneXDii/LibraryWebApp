using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
using System.Linq.Expressions;

namespace LibraryServer.Application.Services.Interfaces;

public interface IAuthorService
{
    Task<ResponseData<List<AuthorDTO>>> ListAllAsync();
    Task<ResponseData<List<AuthorDTO>>> ListAsync(Expression<Func<AuthorDTO, bool>> filter);
    Task<ResponseData<AuthorDTO>> GetByIdAsync(int id);
    Task<ResponseData<AuthorDTO>> AddAsync(AuthorDTO author);
    Task UpdateAsync(int id, AuthorDTO author);
    Task DeleteAsync(int id);
}

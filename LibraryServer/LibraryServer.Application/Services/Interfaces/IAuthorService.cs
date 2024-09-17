using LibraryServer.Application.DTO;
using System.Linq.Expressions;

namespace LibraryServer.Application.Services.Interfaces;

public interface IAuthorService
{
    Task<IReadOnlyList<AuthorDTO>> ListAllAsync();
    Task<IReadOnlyList<AuthorDTO>> ListAsync(Expression<Func<AuthorDTO, bool>> filter);
    Task<AuthorDTO> GetByIdAsync(int id);
    Task AddAsync(AuthorDTO author);
    Task UpdateAsync(int id, AuthorDTO author);
    Task DeleteAsync(AuthorDTO author);
}

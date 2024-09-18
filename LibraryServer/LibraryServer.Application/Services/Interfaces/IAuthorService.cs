using LibraryServer.Application.DTO;

namespace LibraryServer.Application.Services.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorDTO>> ListAllAsync();
    Task<List<BookDTO>> ListAuthorsBooksAsync(int id);
    Task<AuthorDTO> GetByIdAsync(int id);
    Task<AuthorDTO> AddAsync(AuthorDTO author);
    Task UpdateAsync(int id, AuthorDTO author);
    Task DeleteAsync(int id);
}

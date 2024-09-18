using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;

namespace LibraryServer.Application.Services.Interfaces;

public interface IGenreService
{
    Task<List<GenreDTO>> ListAllAsync();
    Task<GenreDTO> GetByIdAsync(int id);
    Task<GenreDTO> AddAsync(GenreDTO genre);
    Task UpdateAsync(int id, GenreDTO genre);
    Task DeleteAsync(int id);
}

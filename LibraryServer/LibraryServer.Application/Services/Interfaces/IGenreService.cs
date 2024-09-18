using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;

namespace LibraryServer.Application.Services.Interfaces;

public interface IGenreService
{
    Task<ResponseData<List<GenreDTO>>> ListAllAsync();
    Task<ResponseData<GenreDTO>> GetByIdAsync(int id);
    Task<ResponseData<GenreDTO>> AddAsync(GenreDTO genre);
    Task UpdateAsync(int id, GenreDTO genre);
    Task DeleteAsync(int id);
}

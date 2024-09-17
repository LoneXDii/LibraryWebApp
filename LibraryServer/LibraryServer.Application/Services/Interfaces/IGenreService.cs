using LibraryServer.Application.DTO;

namespace LibraryServer.Application.Services.Interfaces;

internal interface IGenreService
{
    Task<IReadOnlyList<GenreDTO>> ListAllAsync();
    Task<GenreDTO> GetByIdAsync(int id);
    Task AddAsync(GenreDTO genre);
    Task UpdateAsync(int id, GenreDTO genre);
    Task DeleteAsync(GenreDTO genre);
}

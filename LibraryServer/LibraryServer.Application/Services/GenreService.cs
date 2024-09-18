using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Entities;
using LibraryServer.DataAccess.Repositories.Interfaces;

namespace LibraryServer.Application.Services;

internal class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseData<List<GenreDTO>>> ListAllAsync()
    {
        var genres = (await _unitOfWork.GenreRepository.ListAllAsync()).ToList();
        var genresDto = _mapper.Map<List<GenreDTO>>(genres);

        return ResponseData<List<GenreDTO>>.Success(genresDto);
    }

    public async Task<ResponseData<GenreDTO>> GetByIdAsync(int id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

        if (genre is null)
        {
            return ResponseData<GenreDTO>.Error($"No genre with id={id}");
        }

        var genreDto = _mapper.Map<GenreDTO>(genre);
        return ResponseData<GenreDTO>.Success(genreDto);
    }

    public async Task<ResponseData<GenreDTO>> AddAsync(GenreDTO genre)
    {
        var genreDb = _mapper.Map<Genre>(genre);
        genreDb = await _unitOfWork.GenreRepository.AddAsync(genreDb);
        await _unitOfWork.SaveAllAsync();

        genre = _mapper.Map<GenreDTO>(genreDb);
        return ResponseData<GenreDTO>.Success(genre);
    }

    public async Task UpdateAsync(int id, GenreDTO genre)
    {
        var genreDb = await _unitOfWork.GenreRepository.GetByIdAsync(id);
        if(genreDb is null)
        {
            return;
        }

        genreDb.Name = genre.Name;

        await _unitOfWork.GenreRepository.UpdateAsync(genreDb);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
        if (genre is null)
        {
            return;
        }

        await _unitOfWork.GenreRepository.DeleteAsync(genre);
        await _unitOfWork.SaveAllAsync();
    }
}

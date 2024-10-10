using AutoMapper;
using FluentValidation;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Entities;
using LibraryServer.Domain.Abstactions;
using LibraryServer.DataAccess.Common.Exceptions;
using LibraryServer.Application.Validators;
using System.Text;

namespace LibraryServer.Application.Services;

internal class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly GenreValidator _validator;

    public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = new GenreValidator();
    }

    public async Task<List<GenreDTO>> ListAllAsync()
    {
        var genres = (await _unitOfWork.GenreRepository.ListAllAsync()).ToList();
        var genresDto = _mapper.Map<List<GenreDTO>>(genres);

        return genresDto;
    }

    public async Task<GenreDTO> GetByIdAsync(int id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);

        if (genre is null)
        {
            throw new NotFoundException($"No genre with id={id}");
        }

        var genreDto = _mapper.Map<GenreDTO>(genre);
        return genreDto;
    }

    public async Task<GenreDTO> AddAsync(GenreDTO genre)
    {
        var genreDb = _mapper.Map<Genre>(genre);

        Validate(genreDb);

        genreDb = await _unitOfWork.GenreRepository.AddAsync(genreDb);
        await _unitOfWork.SaveAllAsync();

        genre = _mapper.Map<GenreDTO>(genreDb);
        return genre;
    }

    public async Task UpdateAsync(int id, GenreDTO genre)
    {
        var genreDb = await _unitOfWork.GenreRepository.GetByIdAsync(id);
        if(genreDb is null)
        {
            throw new NotFoundException($"No genre with id={id}");
        }

        genreDb.Name = genre.Name;
        genreDb.NormalizedName = genre.NormalizedName;

        Validate(genreDb);

        await _unitOfWork.GenreRepository.UpdateAsync(genreDb);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
        if (genre is null)
        {
            throw new NotFoundException($"No genre with id={id}");
        }

        await _unitOfWork.GenreRepository.DeleteAsync(genre);
        await _unitOfWork.SaveAllAsync();
    }

    private void Validate(Genre genre)
    {
        var validationResult = _validator.Validate(genre);

        if (!validationResult.IsValid)
        {
            var sb = new StringBuilder();
            foreach (var failure in validationResult.Errors)
            {
                sb.AppendLine(failure.ErrorMessage);
            }
            throw new ValidationException(sb.ToString());
        }
    }
}

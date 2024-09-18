using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Entities;
using LibraryServer.DataAccess.Repositories.Interfaces;

namespace LibraryServer.Application.Services;

internal class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseData<List<AuthorDTO>>> ListAllAsync()
    {
        var authors = (await _unitOfWork.AuthorRepository.ListAllAsync()).ToList();
        var authorsDto = _mapper.Map<List<AuthorDTO>>(authors);

        return ResponseData<List<AuthorDTO>>.Success(authorsDto);
    }

    public async Task<ResponseData<List<BookDTO>>> ListAuthorsBooksAsync(int id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id, a => a.Books);

        if (author is null)
        {
            return ResponseData<List<BookDTO>>.Error($"No author with id={id}");
        }

        var books = _mapper.Map<List<BookDTO>>(author.Books);
        return ResponseData<List<BookDTO>>.Success(books);
    }

    public async Task<ResponseData<AuthorDTO>> GetByIdAsync(int id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (author is null)
        {
            return ResponseData<AuthorDTO>.Error($"No author with id={id}");
        }

        var authorDto = _mapper.Map<AuthorDTO>(author);
        return ResponseData<AuthorDTO>.Success(authorDto);
    }

    public async Task<ResponseData<AuthorDTO>> AddAsync(AuthorDTO author)
    {
        var authorDb = _mapper.Map<Author>(author);
        authorDb = await _unitOfWork.AuthorRepository.AddAsync(authorDb);
        await _unitOfWork.SaveAllAsync();

        author = _mapper.Map<AuthorDTO>(authorDb);
        return ResponseData<AuthorDTO>.Success(author);
    }

    public async Task UpdateAsync(int id, AuthorDTO author)
    {
        var authorDb = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        authorDb.Name = author.Name;
        authorDb.Surname = author.Surname;
        authorDb.Country = author.Country;
        authorDb.DateOfBirth = author.DateOfBirth;

        await _unitOfWork.AuthorRepository.UpdateAsync(authorDb);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author is null)
        {
            return;
        }

        await _unitOfWork.AuthorRepository.DeleteAsync(author);
        await _unitOfWork.SaveAllAsync();
    }
}

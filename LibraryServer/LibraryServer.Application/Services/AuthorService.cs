using AutoMapper;
using FluentValidation;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.Domain.Entities;
using LibraryServer.Domain.Abstactions;
using LibraryServer.Domain.Common.Exceptions;
using LibraryServer.Application.Validators;
using System.Text;

namespace LibraryServer.Application.Services;

internal class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AuthorValidator _validator;
    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = new AuthorValidator();
    }

    public async Task<List<AuthorDTO>> ListAllAsync()
    {
        var authors = (await _unitOfWork.AuthorRepository.ListAllAsync()).ToList();
        var authorsDto = _mapper.Map<List<AuthorDTO>>(authors);
        return authorsDto;
    }

    public async Task<List<BookDTO>> ListAuthorsBooksAsync(int id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id, a => a.Books);

        if (author is null)
        {
            throw new NotFoundException($"No author with id={id}");
        }

        var books = _mapper.Map<List<BookDTO>>(author.Books);
        return books;
    }

    public async Task<AuthorDTO> GetByIdAsync(int id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (author is null)
        {
            throw new NotFoundException($"No author with id={id}");
        }

        var authorDto = _mapper.Map<AuthorDTO>(author);
        return authorDto;
    }

    public async Task<AuthorDTO> AddAsync(AuthorDTO author)
    {
        var authorDb = _mapper.Map<Author>(author);

        Validate(authorDb);

        authorDb = await _unitOfWork.AuthorRepository.AddAsync(authorDb);
        await _unitOfWork.SaveAllAsync();

        author = _mapper.Map<AuthorDTO>(authorDb);
        return author;
    }

    public async Task UpdateAsync(int id, AuthorDTO author)
    {
        var authorDb = await _unitOfWork.AuthorRepository.GetByIdAsync(id);

        if (authorDb is null)
        {
            throw new NotFoundException($"No author with id={id}");
        }

        authorDb.Name = author.Name;
        authorDb.Surname = author.Surname;
        authorDb.Country = author.Country;
        authorDb.DateOfBirth = author.DateOfBirth;

        Validate(authorDb);

        await _unitOfWork.AuthorRepository.UpdateAsync(authorDb);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if (author is null)
        {
            throw new NotFoundException($"No author with id={id}");
        }

        await _unitOfWork.AuthorRepository.DeleteAsync(author);
        await _unitOfWork.SaveAllAsync();
    }

    private void Validate(Author author)
    {
        var validationResult = _validator.Validate(author);

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

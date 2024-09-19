using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Domain.Common.Models;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Repositories.Interfaces;
using LibraryServer.Domain.Entities;
using LibraryServer.Domain.Common.Exceptions;
using System.Linq.Expressions;
using LibraryServer.Application.Validators;
using FluentValidation;
using System.Text;

namespace LibraryServer.Application.Services;

internal class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly BookValidator _validator;
    private readonly int _maxPageSize = 21;

    public BookService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = new BookValidator();
    }

    public async Task<PaginatedListModel<BookDTO>> ListAsync(string? genreNormalizedName, 
                                                        int pageNo = 1, int pageSize = 9)
    {
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        var dataList = new PaginatedListModel<Book>();

        if(genreNormalizedName is not null)
        {
            var genre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(g => g.NormalizedName == genreNormalizedName);
            if (genre is null)
            {
                throw new NotFoundException("No such genre");
            }
            dataList = await _unitOfWork.BookRepository.ListWithPaginationAsync(pageNo, pageSize,
                                                                             b => b.GenreId == genre.Id);
        }
        else
        {
            dataList = (await _unitOfWork.BookRepository.ListWithPaginationAsync(pageNo, pageSize));
        }

        var data = _mapper.Map<PaginatedListModel<BookDTO>>(dataList);
        return data;
    }

    public async Task<BookDTO> GetByIdAsync(int id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (book is null)
        {
            throw new NotFoundException($"No book with id={id}");
        }

        var bookDto = _mapper.Map<BookDTO>(book);
        return bookDto;
    }

    public async Task<BookDTO> FirstOrDefaultAsync(Expression<Func<BookDTO, bool>> filter)
    {
        var bookFilter = _mapper.Map<Expression<Func<Book, bool>>>(filter);

        var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(bookFilter);

        if (book is null)
        {
            throw new NotFoundException("No such book");
        }

        var bookDto = _mapper.Map<BookDTO>(book);
        return bookDto;
    }

    public async Task<BookDTO> AddAsync(BookDTO book)
    {
        var bookDb = _mapper.Map<Book>(book);

        Validate(bookDb);

        bookDb = await _unitOfWork.BookRepository.AddAsync(bookDb);
        await _unitOfWork.SaveAllAsync();

        book = _mapper.Map<BookDTO>(bookDb);
        return book;
    }

    public async Task UpdateAsync(int id, BookDTO book)
    {
        var bookDb = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (bookDb is null)
        {
            throw new NotFoundException($"No book with id={id}");
        }

        bookDb.AuthorId = book.AuthorId;
        bookDb.GenreId = book.GenreId;
        bookDb.ISBN = book.ISBN;
        bookDb.Title = book.Title;
        bookDb.Description = book.Description;
        bookDb.Quantity = book.Quantity;
        bookDb.Image = book.Image;
        bookDb.TimeOfTake = book.TimeOfTake;
        bookDb.TimeToReturn = book.TimeToReturn;

        Validate(bookDb);

        await _unitOfWork.BookRepository.UpdateAsync(bookDb);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book is null)
        {
            throw new NotFoundException($"No book with id={id}");
        }

        await _unitOfWork.BookRepository.DeleteAsync(book);
        await _unitOfWork.SaveAllAsync();
    }

    private void Validate(Book book)
    {
        var validationResult = _validator.Validate(book);

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

using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Repositories.Interfaces;
using LibraryServer.DataAccess.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryServer.Application.Services;

internal class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly int _maxPageSize = 21;

    public BookService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseData<DataListModel<BookDTO>>> ListAsync(string? genreNormalizedName, 
                                                                      int pageNo = 1, int pageSize = 9)
    {
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        List<Book> books;
        var dataList = new DataListModel<BookDTO>();

        if(genreNormalizedName is not null)
        {
            var genre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(g => g.NormalizedName == genreNormalizedName);
            if (genre is null)
            {
                return ResponseData<DataListModel<BookDTO>>.Error("No such genre");
            }
            books = (await _unitOfWork.BookRepository.ListAsync(b => b.GenreId == genre.Id)).ToList();
        }
        else
        {
            books = (await _unitOfWork.BookRepository.ListAllAsync()).ToList();
        }

        var count = books.Count;
        if(count == 0)
        {
            return ResponseData<DataListModel<BookDTO>>.Success(dataList);
        }

        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        if (pageNo > totalPages)
        {
            return ResponseData<DataListModel<BookDTO>>.Error("No such page");
        }

        books = books.OrderBy(b => b.Id)
                     .Skip((pageNo - 1) * pageSize)
                     .Take(pageSize)
                     .ToList();

        dataList.Items = _mapper.Map<List<BookDTO>>(books);
        dataList.CurrentPage = pageNo;
        dataList.TotalPages = totalPages;

        return ResponseData<DataListModel<BookDTO>>.Success(dataList);
    }

    public async Task<ResponseData<BookDTO>> GetByIdAsync(int id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return ResponseData<BookDTO>.Error($"No book with id={id}");
        }

        var bookDto = _mapper.Map<BookDTO>(book);
        return ResponseData<BookDTO>.Success(bookDto);
    }

    public async Task<ResponseData<BookDTO>> FirstOrDefaultAsync(Expression<Func<BookDTO, bool>> filter)
    {
        var bookFilter = _mapper.Map<Expression<Func<Book, bool>>>(filter);
        var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(bookFilter);

        if (book is null)
        {
            return ResponseData<BookDTO>.Error("No such book");
        }

        var bookDto = _mapper.Map<BookDTO>(book);
        return ResponseData<BookDTO>.Success(bookDto);
    }

    public async Task<ResponseData<BookDTO>> AddAsync(BookDTO book)
    {
        var bookDb = _mapper.Map<Book>(book);
        bookDb = await _unitOfWork.BookRepository.AddAsync(bookDb);
        await _unitOfWork.SaveAllAsync();

        book = _mapper.Map<BookDTO>(bookDb);
        return ResponseData<BookDTO>.Success(book);
    }

    public async Task UpdateAsync(int id, BookDTO book)
    {
        var bookDb = await _unitOfWork.BookRepository.GetByIdAsync(id);

        if (bookDb is null)
        {
            return;
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

        await _unitOfWork.BookRepository.UpdateAsync(bookDb);
        await _unitOfWork.SaveAllAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id);
        if (book is null)
        {
            return;
        }

        await _unitOfWork.BookRepository.DeleteAsync(book);
        await _unitOfWork.SaveAllAsync();
    }
}

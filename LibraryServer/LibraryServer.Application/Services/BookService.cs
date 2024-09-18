using AutoMapper;
using LibraryServer.Application.DTO;
using LibraryServer.Application.Models;
using LibraryServer.Application.Services.Interfaces;
using LibraryServer.DataAccess.Repositories.Interfaces;
using LibraryServer.Domain.Entities;
using LibraryServer.Domain.Common.Exceptions;
using System.Linq.Expressions;

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

    public async Task<DataListModel<BookDTO>> ListAsync(string? genreNormalizedName, 
                                                        int pageNo = 1, int pageSize = 9)
    {
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        (IEnumerable<Book>, int) data;
        var dataList = new DataListModel<BookDTO>();

        if(genreNormalizedName is not null)
        {
            var genre = await _unitOfWork.GenreRepository.FirstOrDefaultAsync(g => g.NormalizedName == genreNormalizedName);
            if (genre is null)
            {
                throw new NotFoundException("No such genre");
            }
            data = (await _unitOfWork.BookRepository.ListWithPaginationAsync(pageNo, pageSize,
                                                                             b => b.GenreId == genre.Id));
        }
        else
        {
            data = (await _unitOfWork.BookRepository.ListWithPaginationAsync(pageNo, pageSize));
        }

        dataList.Items = _mapper.Map<List<BookDTO>>(data.Item1.ToList());
        dataList.CurrentPage = pageNo;
        dataList.TotalPages = data.Item2;

        return dataList;
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

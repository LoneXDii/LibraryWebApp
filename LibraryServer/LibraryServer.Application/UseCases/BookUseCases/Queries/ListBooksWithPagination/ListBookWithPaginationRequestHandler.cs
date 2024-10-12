using AutoMapper;

namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

internal class ListBookWithPaginationRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<ListBooksWithPaginationRequest, PaginatedListModel<BookDTO>>
{
    public async Task<PaginatedListModel<BookDTO>> Handle(ListBooksWithPaginationRequest request, 
        CancellationToken cancellationToken = default)
    {
        int _maxPageSize = 21;
        int pageSize = request.PageSize;
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        var dataList = new PaginatedListModel<Book>();

        if (request.GenreNormalizedName is not null)
        {
            var genre = await unitOfWork.GenreRepository.FirstOrDefaultAsync(g => g.NormalizedName == request.GenreNormalizedName);
            if (genre is null)
            {
                throw new NotFoundException("No such genre");
            }
            dataList = await unitOfWork.BookRepository.ListWithPaginationAsync(request.PageNo, pageSize,
                                                                             b => b.GenreId == genre.Id,
                                                                             b => b.Author, b => b.Genre);
        }
        else
        {
            dataList = await unitOfWork.BookRepository.ListWithPaginationAsync(request.PageNo, pageSize, null, 
                b => b.Author, b => b.Genre);
        }

        var data = mapper.Map<PaginatedListModel<BookDTO>>(dataList);
        return data;
    }
}

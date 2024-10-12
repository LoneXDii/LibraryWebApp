using AutoMapper;

namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

internal class GetBookByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetBookByIdRequest, BookDTO>
{
    public async Task<BookDTO> Handle(GetBookByIdRequest request, CancellationToken cancellationToken = default)
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(request.BookId, b => b.Author, b => b.Genre);

        if (book is null)
        {
            throw new NotFoundException($"No book with id={request.BookId}");
        }

        var bookDto = mapper.Map<BookDTO>(book);
        return bookDto;
    }
}

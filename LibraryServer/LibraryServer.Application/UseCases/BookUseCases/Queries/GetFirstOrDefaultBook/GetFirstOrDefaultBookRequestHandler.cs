using AutoMapper;
using System.Linq.Expressions;

namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

internal class GetFirstOrDefaultBookRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetFirstOrDefaultBookRequest, BookDTO>
{
    public async Task<BookDTO> Handle(GetFirstOrDefaultBookRequest request, CancellationToken cancellationToken = default)
    {
        var bookFilter = mapper.Map<Expression<Func<Book, bool>>>(request.Filter);

        var book = await unitOfWork.BookRepository.FirstOrDefaultAsync(bookFilter);

        if (book is null)
        {
            throw new NotFoundException("No such book");
        }

        var bookDto = mapper.Map<BookDTO>(book);
        return bookDto;
    }
}

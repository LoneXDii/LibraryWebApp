using AutoMapper;

namespace LibraryServer.Application.UseCases.AuthorUseCases.Queries;

internal class ListAuthorsBooksRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<ListAuthorsBooksRequest, IEnumerable<BookDTO>>
{
    public async Task<IEnumerable<BookDTO>> Handle(ListAuthorsBooksRequest request, CancellationToken cancellationToken = default)
    {
        var author = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId, a => a.Books);

        if (author is null)
        {
            throw new NotFoundException($"No author with id={request.AuthorId}");
        }

        var books = mapper.Map<List<BookDTO>>(author.Books);
        return books;
    }
}
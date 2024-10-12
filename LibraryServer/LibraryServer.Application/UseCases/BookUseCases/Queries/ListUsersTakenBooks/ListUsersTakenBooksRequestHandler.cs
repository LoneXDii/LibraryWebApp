using AutoMapper;

namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

internal class ListUsersTakenBooksRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<ListUsersTakenBooksRequest, IEnumerable<TakenBookDTO>>
{
    public async Task<IEnumerable<TakenBookDTO>> Handle(ListUsersTakenBooksRequest request, CancellationToken cancellationToken = default)
    {
        var books = await unitOfWork.TakenBookRepository.ListAsync(tb => tb.UserId == request.UserId,
                                                                   tb => tb.Book);

        var response = mapper.Map<List<TakenBookDTO>>(books.ToList());
        return response;
    }
}

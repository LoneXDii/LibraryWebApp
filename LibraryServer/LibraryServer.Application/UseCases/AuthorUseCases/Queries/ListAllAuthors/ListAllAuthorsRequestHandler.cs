using AutoMapper;

namespace LibraryServer.Application.UseCases.AuthorUseCases.Queries;

internal class ListAllAuthorsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<ListAllAuthorsRequest, IEnumerable<AuthorDTO>>
{
    public async Task<IEnumerable<AuthorDTO>> Handle(ListAllAuthorsRequest request, CancellationToken cancellationToken = default)
    {
        var authors = (await unitOfWork.AuthorRepository.ListAllAsync()).ToList();
        var authorsDto = mapper.Map<List<AuthorDTO>>(authors);
        return authorsDto;
    }
}

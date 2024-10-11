using AutoMapper;

namespace LibraryServer.Application.UseCases.GenreUseCases.Queries;

public sealed record ListAllGenresRequest() : IRequest<IEnumerable<GenreDTO>> { }

internal class ListAllGenresRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<ListAllGenresRequest, IEnumerable<GenreDTO>>
{
    public async Task<IEnumerable<GenreDTO>> Handle(ListAllGenresRequest request, 
        CancellationToken cancellationToken = default)
    {
        var genres = (await unitOfWork.GenreRepository.ListAllAsync()).ToList();
        var genresDto = mapper.Map<List<GenreDTO>>(genres);

        return genresDto;
    }
}

using AutoMapper;

namespace LibraryServer.Application.UseCases.GenreUseCases.Queries;

public sealed record GetGenreByIdRequest(int genreId) : IRequest<GenreDTO> { }

internal class GetGenreByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetGenreByIdRequest, GenreDTO>
{
    public async Task<GenreDTO> Handle(GetGenreByIdRequest request, CancellationToken cancellationToken = default)
    {
        var genre = await unitOfWork.GenreRepository.GetByIdAsync(request.genreId);

        if (genre is null)
        {
            throw new NotFoundException($"No genre with id={request.genreId}");
        }

        var genreDto = mapper.Map<GenreDTO>(genre);
        return genreDto;
    }
}


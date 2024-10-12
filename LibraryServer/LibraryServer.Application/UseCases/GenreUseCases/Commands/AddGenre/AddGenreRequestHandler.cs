using AutoMapper;

namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

internal class AddGenreRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<AddGenreRequest, GenreDTO>
{
    public async Task<GenreDTO> Handle(AddGenreRequest request, CancellationToken cancellationToken = default)
    {
        var genreDb = mapper.Map<Genre>(request.Genre);

        //Validate(genreDb);

        genreDb = await unitOfWork.GenreRepository.AddAsync(genreDb);
        await unitOfWork.SaveAllAsync();

        var genre = mapper.Map<GenreDTO>(genreDb);
        return genre;
    }
}
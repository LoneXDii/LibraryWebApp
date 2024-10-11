using AutoMapper;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record AddGenreRequest(GenreDTO genre) : IRequest<GenreDTO> { }

internal class AddGenreRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<AddGenreRequest, GenreDTO>
{
    public async Task<GenreDTO> Handle(AddGenreRequest request, CancellationToken cancellationToken = default)
    {
        var genreDb = mapper.Map<Genre>(request.genre);

        //Validate(genreDb);

        genreDb = await unitOfWork.GenreRepository.AddAsync(genreDb);
        await unitOfWork.SaveAllAsync();

        var genre = mapper.Map<GenreDTO>(genreDb);
        return genre;
    }
}
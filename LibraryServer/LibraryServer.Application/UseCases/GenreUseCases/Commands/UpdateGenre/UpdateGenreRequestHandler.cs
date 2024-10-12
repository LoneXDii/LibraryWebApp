using AutoMapper;

namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

internal class UpdateGenreRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateGenreRequest>
{
    public async Task Handle(UpdateGenreRequest request, CancellationToken cancellationToken = default)
    {
        var genreDb = await unitOfWork.GenreRepository.GetByIdAsync(request.GenreId);
        if (genreDb is null)
        {
            throw new NotFoundException($"No genre with id={request.GenreId}");
        }

        genreDb = mapper.Map<Genre>(request.Genre);
        //Validate(genreDb);

        await unitOfWork.GenreRepository.UpdateAsync(genreDb);
        await unitOfWork.SaveAllAsync();
    }
}
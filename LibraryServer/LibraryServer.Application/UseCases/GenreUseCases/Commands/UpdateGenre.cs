using AutoMapper;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record UpdateGenreRequest(int genreId, GenreDTO genre) : IRequest { }

internal class UpdateGenreRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateGenreRequest>
{
    public async Task Handle(UpdateGenreRequest request, CancellationToken cancellationToken = default)
    {
        var genreDb = await unitOfWork.GenreRepository.GetByIdAsync(request.genreId);
        if (genreDb is null)
        {
            throw new NotFoundException($"No genre with id={request.genreId}");
        }

        genreDb = mapper.Map<Genre>(request.genre);
        //Validate(genreDb);

        await unitOfWork.GenreRepository.UpdateAsync(genreDb);
        await unitOfWork.SaveAllAsync();
    }
}
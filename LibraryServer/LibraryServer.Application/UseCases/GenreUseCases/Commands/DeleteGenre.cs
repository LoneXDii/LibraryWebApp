namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record DeleteGenreRequest(int genreId) : IRequest { }

internal class DeleteGenreRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteGenreRequest>
{
    public async Task Handle(DeleteGenreRequest request, CancellationToken cancellationToken = default)
    {
        var genre = await unitOfWork.GenreRepository.GetByIdAsync(request.genreId);
        if (genre is null)
        {
            throw new NotFoundException($"No genre with id={request.genreId}");
        }

        await unitOfWork.GenreRepository.DeleteAsync(genre);
        await unitOfWork.SaveAllAsync();
    }
}
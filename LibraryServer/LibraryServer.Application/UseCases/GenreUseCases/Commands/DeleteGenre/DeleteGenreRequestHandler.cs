﻿namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

internal class DeleteGenreRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteGenreRequest>
{
    public async Task Handle(DeleteGenreRequest request, CancellationToken cancellationToken = default)
    {
        var genre = await unitOfWork.GenreRepository.GetByIdAsync(request.GenreId);
        if (genre is null)
        {
            throw new NotFoundException($"No genre with id={request.GenreId}");
        }

        await unitOfWork.GenreRepository.DeleteAsync(genre);
        await unitOfWork.SaveAllAsync();
    }
}
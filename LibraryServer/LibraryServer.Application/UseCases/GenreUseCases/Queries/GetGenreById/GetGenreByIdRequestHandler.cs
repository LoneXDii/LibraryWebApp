﻿using AutoMapper;

namespace LibraryServer.Application.UseCases.GenreUseCases.Queries;

internal class GetGenreByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetGenreByIdRequest, GenreDTO>
{
    public async Task<GenreDTO> Handle(GetGenreByIdRequest request, CancellationToken cancellationToken = default)
    {
        var genre = await unitOfWork.GenreRepository.GetByIdAsync(request.GenreId);

        if (genre is null)
        {
            throw new NotFoundException($"No genre with id={request.GenreId}");
        }

        var genreDto = mapper.Map<GenreDTO>(genre);
        return genreDto;
    }
}
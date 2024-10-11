namespace LibraryServer.Application.UseCases.GenreUseCases.Queries;

public sealed record GetGenreByIdRequest(int genreId) : IRequest<GenreDTO> { }
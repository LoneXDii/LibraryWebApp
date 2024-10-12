namespace LibraryServer.Application.UseCases.GenreUseCases.Queries;

public sealed record GetGenreByIdRequest(int GenreId) : IRequest<GenreDTO> { }
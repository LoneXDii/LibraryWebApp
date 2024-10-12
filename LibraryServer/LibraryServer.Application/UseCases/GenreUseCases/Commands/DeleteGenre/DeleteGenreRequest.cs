namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record DeleteGenreRequest(int GenreId) : IRequest { }
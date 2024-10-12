namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record UpdateGenreRequest(int GenreId, GenreDTO Genre) : IRequest { }
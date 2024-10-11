namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record UpdateGenreRequest(int genreId, GenreDTO genre) : IRequest { }
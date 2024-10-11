namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record AddGenreRequest(GenreDTO genre) : IRequest<GenreDTO> { }
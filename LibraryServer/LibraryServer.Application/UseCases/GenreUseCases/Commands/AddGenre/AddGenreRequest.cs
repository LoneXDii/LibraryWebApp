namespace LibraryServer.Application.UseCases.GenreUseCases.Commands;

public sealed record AddGenreRequest(GenreDTO Genre) : IRequest<GenreDTO> { }
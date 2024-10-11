namespace LibraryServer.Application.UseCases.GenreUseCases.Queries;

public sealed record ListAllGenresRequest() : IRequest<IEnumerable<GenreDTO>> { }
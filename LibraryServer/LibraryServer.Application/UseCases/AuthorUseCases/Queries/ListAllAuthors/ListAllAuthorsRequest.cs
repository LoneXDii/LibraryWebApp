namespace LibraryServer.Application.UseCases.AuthorUseCases.Queries;

public sealed record ListAllAuthorsRequest() : IRequest<IEnumerable<AuthorDTO>> { }
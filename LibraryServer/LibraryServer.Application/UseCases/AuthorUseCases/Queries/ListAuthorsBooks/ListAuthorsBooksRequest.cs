namespace LibraryServer.Application.UseCases.AuthorUseCases.Queries;

public sealed record ListAuthorsBooksRequest(int AuthorId) : IRequest<IEnumerable<BookDTO>> { }
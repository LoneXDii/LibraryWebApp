namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

public sealed record ListUsersTakenBooksRequest(string UserId) : IRequest<IEnumerable<TakenBookDTO>> { }
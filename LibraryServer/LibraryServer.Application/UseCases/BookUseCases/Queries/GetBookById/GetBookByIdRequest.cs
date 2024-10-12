namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

public sealed record GetBookByIdRequest(int BookId) : IRequest<BookDTO> { }
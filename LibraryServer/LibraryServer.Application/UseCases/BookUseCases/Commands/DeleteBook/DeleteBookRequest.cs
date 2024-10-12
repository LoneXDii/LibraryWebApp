namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

public sealed record DeleteBookRequest(int BookId) : IRequest { }
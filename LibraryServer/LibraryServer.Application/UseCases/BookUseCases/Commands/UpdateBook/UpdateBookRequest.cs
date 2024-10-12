namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

public sealed record UpdateBookRequest(int BookId, BookWithImageFileDTO BookWithImage) : IRequest { }
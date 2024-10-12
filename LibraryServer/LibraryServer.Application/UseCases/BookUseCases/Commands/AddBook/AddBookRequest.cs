namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

public sealed record AddBookRequest(BookWithImageFileDTO BookWithImage) : IRequest<BookDTO> { }
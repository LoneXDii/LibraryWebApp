namespace LibraryServer.Application.UseCases.AuthorUseCases.Commands;

public sealed record DeleteAuthorRequest(int AuthorId) : IRequest { }
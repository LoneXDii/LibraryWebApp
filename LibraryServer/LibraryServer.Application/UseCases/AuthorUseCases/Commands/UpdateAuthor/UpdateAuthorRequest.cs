namespace LibraryServer.Application.UseCases.AuthorUseCases.Commands;

public sealed record UpdateAuthorRequest(int AuthorId, AuthorDTO Author) : IRequest { }
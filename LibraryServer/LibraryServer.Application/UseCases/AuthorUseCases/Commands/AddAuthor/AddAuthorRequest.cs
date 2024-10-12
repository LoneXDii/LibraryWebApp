namespace LibraryServer.Application.UseCases.AuthorUseCases.Commands;

public sealed record AddAuthorRequest(AuthorDTO Author) : IRequest<AuthorDTO> { }
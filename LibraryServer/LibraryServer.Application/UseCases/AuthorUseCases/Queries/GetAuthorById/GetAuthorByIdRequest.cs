namespace LibraryServer.Application.UseCases.AuthorUseCases.Queries;

public sealed record GetAuthorByIdRequest(int AuthorId) : IRequest<AuthorDTO> { }
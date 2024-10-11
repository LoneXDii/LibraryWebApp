namespace LibraryServer.Application.UseCases.FileUseCases.Commands;

public sealed record DeleteFileRequest(Guid fileId) : IRequest { }
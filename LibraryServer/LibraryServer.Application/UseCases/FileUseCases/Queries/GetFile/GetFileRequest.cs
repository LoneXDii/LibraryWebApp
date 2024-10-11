namespace LibraryServer.Application.UseCases.FileUseCases.Queries;

public sealed record GetFileRequest(Guid fileId) : IRequest<FileResponse> { }
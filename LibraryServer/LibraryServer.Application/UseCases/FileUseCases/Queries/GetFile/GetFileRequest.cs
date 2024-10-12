namespace LibraryServer.Application.UseCases.FileUseCases.Queries;

public sealed record GetFileRequest(Guid FileId) : IRequest<FileResponse> { }
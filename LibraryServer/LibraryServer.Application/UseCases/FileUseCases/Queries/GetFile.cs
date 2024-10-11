namespace LibraryServer.Application.UseCases.FileUseCases.Queries;

public sealed record GetFileRequest(Guid fileId) : IRequest<FileResponse> { }

internal class GetFileRequestHandler(IBlobService blobService)
    : IRequestHandler<GetFileRequest, FileResponse>
{
    public async Task<FileResponse> Handle(GetFileRequest request, CancellationToken cancellationToken = default)
    {
        return await blobService.DownloadAsync(request.fileId);
    }
}
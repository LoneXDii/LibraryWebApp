namespace LibraryServer.Application.UseCases.FileUseCases.Queries;

internal class GetFileRequestHandler(IBlobService blobService)
    : IRequestHandler<GetFileRequest, FileResponse>
{
    public async Task<FileResponse> Handle(GetFileRequest request, CancellationToken cancellationToken = default)
    {
        return await blobService.DownloadAsync(request.fileId);
    }
}
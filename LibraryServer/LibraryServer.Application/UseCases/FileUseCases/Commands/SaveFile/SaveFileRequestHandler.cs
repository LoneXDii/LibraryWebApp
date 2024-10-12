namespace LibraryServer.Application.UseCases.FileUseCases.Commands;

internal class SaveFileRequestHandler(IBlobService blobService)
    : IRequestHandler<SaveFileRequest, Guid>
{
    public async Task<Guid> Handle(SaveFileRequest request, CancellationToken cancellationToken = default)
    {
        using Stream stream = request.File.OpenReadStream();

        return await blobService.UploadAsync(stream, request.File.ContentType);
    }
}
namespace LibraryServer.Application.UseCases.FileUseCases.Commands;

internal class DeleteFileRequestHandler(IBlobService blobService)
    : IRequestHandler<DeleteFileRequest>
{
    public async Task Handle(DeleteFileRequest request, CancellationToken cancellationToken = default)
    {
        await blobService.DeleteAsync(request.FileId);
    }
}
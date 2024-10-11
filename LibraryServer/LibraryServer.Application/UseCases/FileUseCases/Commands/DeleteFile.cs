using LibraryServer.Domain.BlobStorage;

namespace LibraryServer.Application.UseCases.FileUseCases.Commands;

public sealed record DeleteFileRequest(Guid fileId) : IRequest { }

internal class DeleteFileRequestHandler(IBlobService blobService)
    : IRequestHandler<DeleteFileRequest>
{
    public async Task Handle(DeleteFileRequest request, CancellationToken cancellationToken = default)
    {
        await blobService.DeleteAsync(request.fileId);
    }
}
using Microsoft.AspNetCore.Http;

namespace LibraryServer.Application.UseCases.FileUseCases.Commands;

public sealed record SaveFileRequest(IFormFile file) : IRequest<Guid> { }

internal class SaveFileRequestHandler(IBlobService blobService)
    : IRequestHandler<SaveFileRequest, Guid>
{
    public async Task<Guid> Handle(SaveFileRequest request, CancellationToken cancellationToken = default)
    {
        using Stream stream = request.file.OpenReadStream();

        return await blobService.UploadAsync(stream, request.file.ContentType);
    }
}
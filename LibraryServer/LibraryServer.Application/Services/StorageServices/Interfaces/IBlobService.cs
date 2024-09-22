namespace LibraryServer.Application.Services.StorageServices.Interfaces;

public interface IBlobService
{
    Task<Guid> UploadAsync(Stream stream, string contentType);
    Task<FileResponse> DownloadAsync(Guid fileId);
    Task DeleteAsync(Guid fileId);
}

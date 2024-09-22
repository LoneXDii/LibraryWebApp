namespace LibraryServer.Application.Services.StorageServices.Interfaces;

public record FileResponse(Stream Stream, string ContentType);
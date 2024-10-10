namespace LibraryServer.Domain.BlobStorage;

public record FileResponse(Stream Stream, string ContentType);
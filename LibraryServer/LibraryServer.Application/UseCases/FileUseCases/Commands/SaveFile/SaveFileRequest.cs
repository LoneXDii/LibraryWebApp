using Microsoft.AspNetCore.Http;

namespace LibraryServer.Application.UseCases.FileUseCases.Commands;

public sealed record SaveFileRequest(IFormFile file) : IRequest<Guid> { }
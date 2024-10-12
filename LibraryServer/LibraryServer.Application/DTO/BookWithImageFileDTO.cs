using Microsoft.AspNetCore.Http;

namespace LibraryServer.Application.DTO;

public class BookWithImageFileDTO
{
    public BookDTO Book { get; set; }
    public IFormFile? ImageFile { get; set; }
}

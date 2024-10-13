using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

internal class AddBookRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, 
    IBlobService blobService, IConfiguration cfg)
    : IRequestHandler<AddBookRequest, BookDTO>
{
    public async Task<BookDTO> Handle(AddBookRequest request, CancellationToken cancellationToken = default)
    {
        var bookDb = mapper.Map<Book>(request.BookWithImage.Book);

        if(request.BookWithImage.ImageFile is not null)
        {
            using Stream stream = request.BookWithImage.ImageFile.OpenReadStream();
            var imageId = await blobService.UploadAsync(stream, request.BookWithImage.ImageFile.ContentType);

            var imageUrl = (cfg["IMAGE_PATH"] ?? "https://localhost:7001/api/files/") + imageId.ToString();
            bookDb.Image = imageUrl;
        }

        bookDb = await unitOfWork.BookRepository.AddAsync(bookDb);
        await unitOfWork.SaveAllAsync();

        var book = mapper.Map<BookDTO>(bookDb);
        return book;
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

internal class AddBookRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, 
    IBlobService blobService, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<AddBookRequest, BookDTO>
{
    public async Task<BookDTO> Handle(AddBookRequest request, CancellationToken cancellationToken = default)
    {
        var bookDb = mapper.Map<Book>(request.BookWithImage.Book);

        if(request.BookWithImage.ImageFile is not null)
        {
            using Stream stream = request.BookWithImage.ImageFile.OpenReadStream();
            var imageId = await blobService.UploadAsync(stream, request.BookWithImage.ImageFile.ContentType);

            var context = httpContextAccessor.HttpContext;
            var imageUrl = $"{context.Request.Scheme}://{context.Request.Host}/api/files/{imageId}";
            bookDb.Image = imageUrl;
        }
        //Validate(bookDb);

        bookDb = await unitOfWork.BookRepository.AddAsync(bookDb);
        await unitOfWork.SaveAllAsync();

        var book = mapper.Map<BookDTO>(bookDb);
        return book;
    }
}

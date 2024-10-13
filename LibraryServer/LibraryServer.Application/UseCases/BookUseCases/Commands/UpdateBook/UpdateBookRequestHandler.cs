using AutoMapper;
using LibraryServer.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

internal class UpdateBookRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, 
    IBlobService blobService, IConfiguration cfg)
    : IRequestHandler<UpdateBookRequest>
{
    public async Task Handle(UpdateBookRequest request, CancellationToken cancellationToken = default)
    {
        var bookDb = await unitOfWork.BookRepository.GetByIdAsync(request.BookId);

        if (bookDb is null)
        {
            throw new NotFoundException($"No book with id={request.BookId}");
        }

        if(request.BookWithImage.ImageFile is not null)
        {
            if (request.BookWithImage.Book.Image is not null)
            {
                var imageDelId = request.BookWithImage.Book.Image.Split('/').Last();
                await blobService.DeleteAsync(new Guid(imageDelId));
            }
            using Stream stream = request.BookWithImage.ImageFile.OpenReadStream();
            var imageId = await blobService.UploadAsync(stream, request.BookWithImage.ImageFile.ContentType);

            var imageUrl = (cfg["IMAGE_PATH"] ?? "https://localhost:7001/api/files/") + imageId.ToString();
            request.BookWithImage.Book.Image = imageUrl;
        }

        mapper.Map(request.BookWithImage.Book, bookDb);
        //Validate(bookDb);

        await unitOfWork.BookRepository.UpdateAsync(bookDb);
        await unitOfWork.SaveAllAsync();
    }
}

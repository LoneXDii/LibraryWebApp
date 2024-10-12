namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

internal class DeleteBookRequestHandler(IUnitOfWork unitOfWork, IBlobService blobService)
    : IRequestHandler<DeleteBookRequest>
{
    public async Task Handle(DeleteBookRequest request, CancellationToken cancellationToken = default)
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(request.BookId);
        if (book is null)
        {
            throw new NotFoundException($"No book with id={request.BookId}");
        }
        if(book.Image is not null)
        {
            var imageId = book.Image.Split('/').Last();
            await blobService.DeleteAsync(new Guid(imageId));
        }

        await unitOfWork.BookRepository.DeleteAsync(book);
        await unitOfWork.SaveAllAsync();
    }
}

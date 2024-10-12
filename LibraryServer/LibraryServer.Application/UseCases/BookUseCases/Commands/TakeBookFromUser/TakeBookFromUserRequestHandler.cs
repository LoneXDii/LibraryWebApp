namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

internal class TakeBookFromUserRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<TakeBookFromUserRequest>
{
    public async Task Handle(TakeBookFromUserRequest request, CancellationToken cancellationToken = default)
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(request.TakeBookObj.BookId);
        if (book is null)
        {
            throw new NotFoundException($"No book with id={request.TakeBookObj.BookId}");
        }

        var takenBook = await unitOfWork.TakenBookRepository.FirstOrDefaultAsync(b => b.Id == request.TakeBookObj.BookId 
            && b.UserId == request.TakeBookObj.UserId);

        if (takenBook is null)
        {
            throw new BadRequestException($"No such order in library");
        }

        book.Quantity++;
        await unitOfWork.BookRepository.UpdateAsync(book);
        await unitOfWork.TakenBookRepository.DeleteAsync(takenBook);
        await unitOfWork.SaveAllAsync();
    }
}

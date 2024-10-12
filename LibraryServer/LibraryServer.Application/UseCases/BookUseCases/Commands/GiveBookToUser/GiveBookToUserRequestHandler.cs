namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

internal class GiveBookToUserRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GiveBookToUserRequest>
{
    public async Task Handle(GiveBookToUserRequest request, CancellationToken cancellationToken = default)
    {
        var book = await unitOfWork.BookRepository.GetByIdAsync(request.GiveBookObj.BookId);
        if (book is null)
        {
            throw new NotFoundException($"No book with id={request.GiveBookObj.BookId}");
        }

        if (book.Quantity <= 0)
        {
            throw new BadRequestException($"Book with id={request.GiveBookObj.BookId} is out of stock");
        }

        book.Quantity--;
        await unitOfWork.BookRepository.UpdateAsync(book);

        var takenBook = new TakenBook
        {
            BookId = book.Id,
            UserId = request.GiveBookObj.UserId,
            TimeOfTake = DateTime.Now,
            TimeToReturn = DateTime.Now.AddDays(14)
        };

        await unitOfWork.TakenBookRepository.AddAsync(takenBook);
        await unitOfWork.SaveAllAsync();
    }
}

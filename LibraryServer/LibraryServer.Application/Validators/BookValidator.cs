using FluentValidation;

namespace LibraryServer.Application.Validators;

public class BookValidator : AbstractValidator<BookWithImageFileDTO>
{
    private readonly IUnitOfWork _unitOfWork;

    public BookValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(b => b.Book.ISBN)
            .NotEmpty()
            .Matches(@"^:?\x20*(?=.{17}$)97(?:8|9)([ -])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$")
            .MustAsync(BeAnUniqueISBN)
            .WithMessage("Incorrect ISBN (Must match an ISBN pattern and be unique)");

        RuleFor(b => b.Book.Title)
            .NotEmpty()
            .WithMessage("Books's title is required");

        RuleFor(b => b.Book.Description)
            .NotEmpty()
            .WithMessage("Book's description is required");

        RuleFor(b => b.Book.Quantity)
            .GreaterThan(-1)
            .WithMessage("Incorrect quantity");
    }

    private async Task<bool> BeAnUniqueISBN(string ISBN, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.BookRepository.FirstOrDefaultAsync(b => b.ISBN == ISBN);
        return book is null;
    }
}

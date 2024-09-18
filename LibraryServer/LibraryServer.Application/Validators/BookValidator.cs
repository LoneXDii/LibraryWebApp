using FluentValidation;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.Validators;

internal class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(b => b.ISBN)
            .NotEmpty()
            .Matches(@"^:?\x20*(?=.{17}$)97(?:8|9)([ -])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$")
            .WithMessage("Incorrect ISBN");

        RuleFor(b => b.Title)
            .NotEmpty()
            .WithMessage("Books's title is required");

        RuleFor(b => b.Description)
            .NotEmpty()
            .WithMessage("Book's description is required");

        RuleFor(b => b.Quantity)
            .GreaterThan(0)
            .WithMessage("Incorrect quantity");
    }

}

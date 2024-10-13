﻿using FluentValidation;

namespace LibraryServer.Application.Validators;

public class BookValidator : AbstractValidator<BookWithImageFileDTO>
{
    public BookValidator()
    {
        RuleFor(b => b.Book.ISBN)
            .NotEmpty()
            .Matches(@"^:?\x20*(?=.{17}$)97(?:8|9)([ -])\d{1,5}\1\d{1,7}\1\d{1,6}\1\d$")
            .WithMessage("Incorrect ISBN");

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
}

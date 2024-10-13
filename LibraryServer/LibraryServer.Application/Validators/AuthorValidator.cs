﻿using FluentValidation;

namespace LibraryServer.Application.Validators;

public class AuthorValidator : AbstractValidator<AuthorDTO>
{
    public AuthorValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Author's name is required");

        RuleFor(a => a.Surname)
            .NotEmpty()
            .WithMessage("Author's surname is required");

        RuleFor(a => a.Country)
            .NotEmpty()
            .WithMessage("Author's country is required");

        RuleFor(a => a.DateOfBirth)
            .Must(BeAValidBirthDate)
            .WithMessage("Incorrect birth date");
    }

    private bool BeAValidBirthDate(DateTime birthDate)
    {
        return (birthDate.AddYears(18) <= DateTime.Today);
    }
}

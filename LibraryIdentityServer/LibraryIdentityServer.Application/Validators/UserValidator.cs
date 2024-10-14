using FluentValidation;
using LibraryIdentityServer.Application.Models;

namespace LibraryIdentityServer.Application.Validators;

public class UserValidator : AbstractValidator<RegisterModel>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("Incorrect name");

        RuleFor(u => u.Surname)
            .NotEmpty()
            .WithMessage("Incorrect surname");

        RuleFor(u => u.Email)
            .NotEmpty()
            .Matches(@"^\S+@\S+\.\S+$")
            .WithMessage("Incorrect email adress");

        RuleFor(u => u.Password)
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Password must contains lower and uppercase letters, at least 1 digit and special symbol");

        RuleFor(u => u.Phone)
            .NotEmpty()
            .Matches(@"^[\+][(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")
            .WithMessage("Incorrect phone number");
    }
}

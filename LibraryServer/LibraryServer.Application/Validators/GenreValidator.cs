using FluentValidation;
using LibraryServer.DataAccess.Entities;
using System.Text.RegularExpressions;

namespace LibraryServer.Application.Validators;

internal class GenreValidator : AbstractValidator<Genre>
{
    public GenreValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty()
            .WithMessage("Genre name is required");

        RuleFor(g => g.NormalizedName)
            .NotEmpty()
            .Must(BeAValidNormalizedName)
            .WithMessage("Incorrect genre's normalized name");
    }

    private bool BeAValidNormalizedName(string normalizedName)
    {
        if (string.IsNullOrEmpty(normalizedName))
        {
            return false;
        }
        var regex = new Regex(@"^([a-z][a-z0-9]*)(-[a-z0-9]+)*$");
        return regex.IsMatch(normalizedName);
    }
}

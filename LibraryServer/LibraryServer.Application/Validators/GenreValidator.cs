using FluentValidation;
using System.Text.RegularExpressions;

namespace LibraryServer.Application.Validators;

public class GenreValidator : AbstractValidator<GenreDTO>
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

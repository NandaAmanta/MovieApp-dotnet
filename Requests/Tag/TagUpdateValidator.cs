

using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Data;

namespace MovieApp.Requests.Tags;

public class TagUpdateValidator : AbstractValidator<TagUpdateRequest>
{
    private readonly MovieAppDataContext _context;
    public TagUpdateValidator(MovieAppDataContext context)
    {
        _context = context;
        RuleFor(t => t.Name).NotEmpty().NotNull().Must(BeUniqueName);
        RuleFor(t => t.Name).Must(BeUniqueName).WithMessage("Tag name must be unique");
    }

    private bool BeUniqueName(string? name)
    {
        return this._context.Tag.Where(t => t.Name == name).IsNullOrEmpty();
    }
}
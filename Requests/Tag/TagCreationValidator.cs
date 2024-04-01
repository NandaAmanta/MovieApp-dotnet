

using FluentValidation;

namespace MovieApp.Requests.Tags;

public class TagCreationValidator : AbstractValidator<TagCreationRequest>
{
    public TagCreationValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
    }
}
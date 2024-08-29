using FluentValidation;
using URLShortener.Resources;

namespace URLShortener.Application.Features.Commands.CreateURL;

public class CreateURLCommandValidator : AbstractValidator<CreateURLCommand>
{
    public CreateURLCommandValidator()
    {
        RuleFor(x => x.Original).Cascade(CascadeMode.Stop)
            .Must(x => !string.IsNullOrEmpty(x)).WithMessage(Messages.URLShouldNotBeNullOrEmpty)
            .Must(x => x.StartsWith("http")).WithMessage(Messages.InvalidURL);
    }
}

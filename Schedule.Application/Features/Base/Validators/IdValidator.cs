using FluentValidation;

namespace Schedule.Application.Features.Base.Validators;

public sealed class IdValidator : AbstractValidator<int>
{
    public IdValidator()
    {
        RuleFor(id => id)
            .GreaterThan(0);
    }
}
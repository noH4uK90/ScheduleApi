using FluentValidation;

namespace Schedule.Application.Features.Base.Validators;

public sealed class IdsValidator : AbstractValidator<ICollection<int>>
{
    public IdsValidator()
    {
        var idValidator = new IdValidator();

        RuleFor(ids => ids)
            .Must(ids => ids.All(id => idValidator.Validate(id).IsValid));
    }
}
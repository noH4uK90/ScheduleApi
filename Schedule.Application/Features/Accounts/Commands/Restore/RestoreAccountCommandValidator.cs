using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Accounts.Commands.Restore;

public class RestoreAccountCommandValidator : AbstractValidator<RestoreAccountCommand>
{
    public RestoreAccountCommandValidator()
    {
        RuleFor(command => command.Id)
            .SetValidator(new IdValidator());
    }
}
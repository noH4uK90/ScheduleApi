using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Accounts.Commands.ChangePassword;

public class ChangeAccountPasswordCommandValidator : AbstractValidator<ChangeAccountPasswordCommand>
{
    public ChangeAccountPasswordCommandValidator()
    {
        RuleFor(command => command.Id)
            .SetValidator(new IdValidator());
        RuleFor(command => command.Password)
            .Length(8, 100)
            .NotEmpty();
        RuleFor(command => command.NewPassword)
            .Length(8, 100)
            .NotEmpty();
    }
}
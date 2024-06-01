using FluentValidation;

namespace Schedule.Application.Features.Accounts.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Login)
            .Length(4, 50)
            .NotEmpty();
        RuleFor(command => command.Password)
            .Length(4, 100)
            .NotEmpty();
    }
}
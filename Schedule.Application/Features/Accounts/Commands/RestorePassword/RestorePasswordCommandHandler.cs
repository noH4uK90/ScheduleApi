using MediatR;
using MimeKit.Text;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.RestorePassword;

public sealed class RestorePasswordCommandHandler(
    IAccountRepository accountRepository,
    IPasswordGenerator passwordGenerator,
    IMailSenderService senderService) : IRequestHandler<RestorePasswordCommand, Unit>
{
    public async Task<Unit> Handle(RestorePasswordCommand request, CancellationToken cancellationToken)
    {
        const int passwordLength = 12;
        var password = passwordGenerator.Generate(passwordLength);
        await accountRepository.RestorePasswordAsync(request.Email, password, cancellationToken);

        await senderService.SendAsync(new Letter
        {
            From = "Электронное расписание",
            To = request.Email,
            Subject = "Восстановление пароля",
            Message = $"Новый пароль: {password}",
            Format = TextFormat.Text
        }, cancellationToken);
        
        return Unit.Value;
    }
}
using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.ChangePassword;

public sealed class ChangeAccountPasswordCommandHandler(
    IAccountRepository accountRepository) : IRequestHandler<ChangeAccountPasswordCommand, Unit>
{
    public async Task<Unit> Handle(ChangeAccountPasswordCommand request, CancellationToken cancellationToken)
    {
        await accountRepository.ChangePasswordAsync(request.Id, request.Password, request.NewPassword, cancellationToken);
        return Unit.Value;
    }
}
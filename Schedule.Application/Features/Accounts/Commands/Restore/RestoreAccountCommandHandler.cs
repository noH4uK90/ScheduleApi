using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.Restore;

public sealed class RestoreAccountCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<RestoreAccountCommand, Unit>
{
    public async Task<Unit> Handle(RestoreAccountCommand request, CancellationToken cancellationToken)
    {
        await accountRepository.RestoreAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
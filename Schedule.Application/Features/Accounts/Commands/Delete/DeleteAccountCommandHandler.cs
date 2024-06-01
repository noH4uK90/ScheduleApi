using MediatR;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.Delete;

public sealed class DeleteAccountCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<DeleteAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await accountRepository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
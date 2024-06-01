using MediatR;

namespace Schedule.Application.Features.Accounts.Commands.Restore;

public sealed record RestoreAccountCommand(int Id) : IRequest<Unit>;
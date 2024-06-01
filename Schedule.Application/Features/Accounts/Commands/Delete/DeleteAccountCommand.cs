using MediatR;

namespace Schedule.Application.Features.Accounts.Commands.Delete;

public sealed record DeleteAccountCommand(int Id) : IRequest<Unit>;
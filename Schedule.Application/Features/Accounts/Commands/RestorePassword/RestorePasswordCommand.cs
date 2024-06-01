using MediatR;

namespace Schedule.Application.Features.Accounts.Commands.RestorePassword;

public sealed class RestorePasswordCommand : IRequest<Unit>
{
    public required string Email { get; set; }
}
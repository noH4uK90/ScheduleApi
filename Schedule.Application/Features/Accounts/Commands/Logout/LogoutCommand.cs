using MediatR;

namespace Schedule.Application.Features.Accounts.Commands.Logout;

public sealed class LogoutCommand : IRequest<Unit>
{
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }

    public bool IsAllDevices { get; set; } = false;
}
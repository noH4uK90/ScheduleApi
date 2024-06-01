using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Accounts.Commands.Refresh;

public sealed class RefreshCommand : IRequest<AuthorizationResultViewModel>
{
    public required string RefreshToken { get; set; }
    public required string AccessToken { get; set; }
}
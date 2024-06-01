using AutoMapper;
using MediatR;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Application.Features.Accounts.Commands.Login;

public sealed class LoginCommandHandler(
    IScheduleDbContext context,
    IAccountRepository accountRepository,
    ISessionRepository sessionRepository,
    ITokenService tokenService,
    IMapper mapper,
    IPasswordHasherService passwordHasher)
    : IRequestHandler<LoginCommand, AuthorizationResultViewModel>
{
    public async Task<AuthorizationResultViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await context.WithTransactionAsync(async () =>
        {
            var account = await accountRepository.FindByLoginAsync(request.Login, cancellationToken);

            if (account is null || !passwordHasher.EnhancedHash(request.Password, account.PasswordHash))
            {
                throw new IncorrectAuthorizationDataException();
            }

            var session = new Session
            {
                SessionId = Guid.NewGuid(),
                RefreshToken = tokenService.GenerateRefreshToken(),
                AccountId = account.AccountId
            };

            await sessionRepository.CreateAsync(session, cancellationToken);

            var accountViewModel = mapper.Map<AccountViewModel>(account);
            var accessToken = tokenService.GenerateAccessToken(account, session.SessionId);

            return new AuthorizationResultViewModel
            {
                AccessToken = accessToken,
                RefreshToken = session.RefreshToken,
                Account = accountViewModel
            };
        }, cancellationToken);
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Queries.Get;

public sealed class GetAccountQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetAccountQuery, AccountViewModel>
{
    public async Task<AccountViewModel> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .AsNoTracking()
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Students)
            .Include(e => e.Teachers)
            .FirstOrDefaultAsync(e => e.AccountId == request.Id, cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.Id);

        return mapper.Map<AccountViewModel>(account);
    }
}
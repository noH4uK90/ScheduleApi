using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Accounts.Queries.Get;

public sealed record GetAccountQuery(int Id) : IRequest<AccountViewModel>;
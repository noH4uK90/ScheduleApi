using MediatR;
using Schedule.Application.Common.Enums;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Queries.GetList;

public sealed record GetAccountListQuery : PaginatedQuery, IRequest<PagedList<AccountViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
    public AccountRole? Role { get; init; }
    public string? Search { get; set; }
}
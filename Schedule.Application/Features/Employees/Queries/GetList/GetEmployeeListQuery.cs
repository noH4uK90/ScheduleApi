using MediatR;
using Schedule.Application.Common.Enums;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Employees.Queries.GetList;

public sealed record GetEmployeeListQuery : PaginatedQuery, IRequest<PagedList<EmployeeViewModel>>
{
   public required QueryFilter Filter { get; init; } = QueryFilter.Available;
    public string? Search { get; set; }
}
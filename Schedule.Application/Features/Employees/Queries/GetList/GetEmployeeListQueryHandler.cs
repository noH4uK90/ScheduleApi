using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Employees.Queries.GetList;

public sealed class GetEmployeeListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetEmployeeListQuery, PagedList<EmployeeViewModel>>
{
    public async Task<PagedList<EmployeeViewModel>> Handle(GetEmployeeListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Employees
            .Include(e => e.Account)
            .ThenInclude(e => e.Role)
            .AsNoTracking();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.Account.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.Account.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
        {
            query = query.Where(e =>
                e.Account.Login.StartsWith(request.Search) ||
                e.Account.Email.StartsWith(request.Search) ||
                e.Account.Name.StartsWith(request.Search) ||
                e.Account.Surname.StartsWith(request.Search) ||
                e.Account.MiddleName != null && e.Account.MiddleName.StartsWith(request.Search));
        }

        var employees = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<EmployeeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<EmployeeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = employees
        };
    }
}
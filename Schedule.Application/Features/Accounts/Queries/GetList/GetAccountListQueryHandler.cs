using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Queries.GetList;

public sealed class GetAccountListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetAccountListQuery, PagedList<AccountViewModel>>
{
    public async Task<PagedList<AccountViewModel>> Handle(GetAccountListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Accounts
            .AsNoTracking();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Role is not null)
        {
            query = query.Where(e => e.RoleId == (int)request.Role);
        }

        var accounts = await query
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Students)
            .Include(e => e.Teachers)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<AccountViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<AccountViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = accounts
        };
    }
}
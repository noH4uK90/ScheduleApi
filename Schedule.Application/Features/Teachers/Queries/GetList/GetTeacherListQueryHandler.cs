using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.GetList;

public sealed class GetTeacherListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetTeacherListQuery, PagedList<TeacherViewModel>>
{
    public async Task<PagedList<TeacherViewModel>> Handle(GetTeacherListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Teachers
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
        
        var teachers = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<TeacherViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<TeacherViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = teachers
        };
    }
}
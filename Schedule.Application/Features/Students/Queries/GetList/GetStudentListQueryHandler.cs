using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Common.Enums;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Queries.GetList;

public sealed class GetStudentListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetStudentListQuery, PagedList<StudentViewModel>>
{
    public async Task<PagedList<StudentViewModel>> Handle(GetStudentListQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Students
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

        var students = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<StudentViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<StudentViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = students
        };
    }
}
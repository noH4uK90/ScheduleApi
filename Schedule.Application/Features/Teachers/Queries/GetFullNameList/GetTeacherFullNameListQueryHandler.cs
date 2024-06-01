using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Teachers.Queries.GetFullNameList;

public class GetTeacherFullNameListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) 
    : IRequestHandler<GetTeacherFullNameListQuery, ICollection<TeacherFullNameViewModel>>
{
    public async Task<ICollection<TeacherFullNameViewModel>> Handle(GetTeacherFullNameListQuery request, CancellationToken cancellationToken)
    {
        var query = context.TeacherFullNames
            .Skip(1)
            .AsSplitQuery()
            .AsNoTracking();

        if (request.Search is not null)
        {
            query = query
                .Where(e => e.FullName.StartsWith(request.Search));
        }

        return await query
            .ProjectTo<TeacherFullNameViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
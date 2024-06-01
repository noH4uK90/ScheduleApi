using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Timetables.Query.GetForGroup;

public class GetTimetableForGroupQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) 
    : IRequestHandler<GetTimetableForGroupQuery, ICollection<TimetableViewModel>>
{
    public async Task<ICollection<TimetableViewModel>> Handle(GetTimetableForGroupQuery request, CancellationToken cancellationToken)
    {
        return await context.Timetables
            .AsNoTracking()
            .Include(e => e.Group)
            .Include(e => e.Day)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Type)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Type)
            .Where(e =>
                e.GroupId == request.GroupId &&
                e.Date == request.Date)
            .ProjectTo<TimetableViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
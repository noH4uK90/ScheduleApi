using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Timetables.Query.GetForTeacher;

public class GetTimetableForTeacherQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) 
    : IRequestHandler<GetTimetableForTeacherQuery, ICollection<LessonViewModel>>
{
    public async Task<ICollection<LessonViewModel>> Handle(GetTimetableForTeacherQuery request, CancellationToken cancellationToken)
    {
        return await context.Lessons
            .AsNoTracking()
            .Include(e => e.Timetable)
            .Include(e => e.Discipline)
            .Include(e => e.Teacher)
            .Include(e => e.Classroom)
            .Include(e => e.Type)
            .Include(e => e.SubLesson)
                .ThenInclude(e => e.Discipline)
            .Include(e => e.SubLesson)
                .ThenInclude(e => e.Teacher)
            .Include(e => e.SubLesson)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.SubLesson)
                .ThenInclude(e => e.Type)
            .Where(e =>
                e.Timetable.Date == request.Date &&
                e.TeacherId == request.TeacherFullNameId)
            .ProjectTo<LessonViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
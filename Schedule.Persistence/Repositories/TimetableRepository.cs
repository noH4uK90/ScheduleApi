using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TimetableRepository(
    IScheduleDbContext context) : ITimetableRepository
{
    public async Task<Timetable> AddIfNotExists(Timetable timetable, CancellationToken cancellationToken = default)
    {
        var timetableDb = await context.Timetables
            .Include(e => e.Day)
            .Include(e => e.Group)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.Type)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Classroom)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Discipline)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Teacher)
            .Include(e => e.Lessons)
                .ThenInclude(e => e.SubLesson)
                .ThenInclude(e => e.Type)
            .FirstOrDefaultAsync(e =>
                e.DayId == timetable.DayId &&
                e.GroupId == timetable.GroupId &&
                e.Date == timetable.Date, cancellationToken);

        if (timetableDb is not null) return timetableDb;

        var created = await context.Timetables
            .AddAsync(timetable, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return created.Entity;
    }
}
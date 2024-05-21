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
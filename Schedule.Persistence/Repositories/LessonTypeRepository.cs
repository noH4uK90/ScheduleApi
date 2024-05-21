using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class LessonTypeRepository(
    IScheduleDbContext context) : ILessonTypeRepository
{
    public async Task<LessonType> AddIfNotExists(string name, CancellationToken cancellationToken = default)
    {
        var lessonType = await context.LessonTypes
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

        if (lessonType is not null) return lessonType;

        var created = await context.LessonTypes
            .AddAsync(new LessonType
            {
                Name = name
            }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return created.Entity;
    }
}
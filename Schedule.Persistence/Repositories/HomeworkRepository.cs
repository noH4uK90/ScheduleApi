using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class HomeworkRepository(
    IScheduleDbContext context) : IHomeworkRepository
{
    public async Task<int> CreateAsync(Homework homework, CancellationToken cancellationToken = default)
    {
        var created = await context.Homeworks
            .AddAsync(homework, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return created.Entity.HomeworkId;
    }

    public async Task DeleteAsync(int homeworkId, CancellationToken cancellationToken = default)
    {
        var homeworkDb = await context.Homeworks
            .FirstOrDefaultAsync(e => e.HomeworkId == homeworkId, cancellationToken);

        if (homeworkDb is null)
        {
            throw new NotFoundException(nameof(Homework), homeworkId);
        }

        context.Homeworks.Remove(homeworkDb);
        await context.SaveChangesAsync(cancellationToken);
    }
}
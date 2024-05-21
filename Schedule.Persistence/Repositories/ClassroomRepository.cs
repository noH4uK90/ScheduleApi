using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class ClassroomRepository(
    IScheduleDbContext context) : IClassroomRepository
{
    public async Task<Classroom> AddIfNotExists(string cabinet, CancellationToken cancellationToken = default)
    {
        var classroom = await context.Classrooms
            .FirstOrDefaultAsync(e => e.Cabinet == cabinet, cancellationToken);

        if (classroom is not null) return classroom;
        
        var created = await context.Classrooms
            .AddAsync(new Classroom
            {
                Cabinet = cabinet
            }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return created.Entity;
    }
}
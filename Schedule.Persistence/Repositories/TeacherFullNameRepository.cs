using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TeacherFullNameRepository(
    IScheduleDbContext context) : ITeacherFullNameRepository
{
    public async Task<TeacherFullName> AddIfNotExists(string fullName, CancellationToken cancellationToken = default)
    {
        var teacher = await context.TeacherFullNames
            .FirstOrDefaultAsync(e => e.FullName == fullName, cancellationToken);

        if (teacher is not null) return teacher;

        var created = await context.TeacherFullNames
            .AddAsync(new TeacherFullName
            {
                FullName = fullName
            }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return created.Entity;
    }
}
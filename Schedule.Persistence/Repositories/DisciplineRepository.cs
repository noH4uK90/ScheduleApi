using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class DisciplineRepository(
    IScheduleDbContext context) : IDisciplineRepository
{
    public async Task<Discipline> AddIfNotExists(string name, CancellationToken cancellationToken = default)
    {
        var discipline = await context.Disciplines
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);

        if (discipline is not null) return discipline;

        var created = await context.Disciplines
            .AddAsync(new Discipline
            {
                Name = name
            }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return created.Entity;
    }

    public async Task AddGroupDiscipline(int groupId, string name, CancellationToken cancellationToken = default)
    {
        var discipline = await AddIfNotExists(name, cancellationToken);

        var groupDiscipline = await context.GroupDisciplines
            .FirstOrDefaultAsync(e =>
                e.DisciplineId == discipline.DisciplineId &&
                e.GroupId == groupId, cancellationToken);
        
        if (groupDiscipline is not null) return;

        await context.GroupDisciplines
            .AddAsync(new GroupDiscipline
            {
                GroupId = groupId,
                DisciplineId = discipline.DisciplineId
            }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
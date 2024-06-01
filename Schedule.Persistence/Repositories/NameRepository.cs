using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class NameRepository(IScheduleDbContext context) : INameRepository
{
    public async Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default)
    {
        var nameDb = await context.Names
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Value == name, cancellationToken);

        if (nameDb is not null)
        {
            return;
        }

        await context.Names.AddAsync(new Name
        {
            Value = name
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
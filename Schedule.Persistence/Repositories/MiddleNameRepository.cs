using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class MiddleNameRepository(IScheduleDbContext context) : IMiddleNameRepository
{
    public async Task AddIfNotExistAsync(string middleName, CancellationToken cancellationToken = default)
    {
        var nameDb = await context.MiddleNames
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Value == middleName, cancellationToken);

        if (nameDb is not null)
        {
            return;
        }

        await context.MiddleNames.AddAsync(new MiddleName
        {
            Value = middleName
        }, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class SessionRepository(
    IScheduleDbContext context,
    IDateInfoService dateInfoService) : ISessionRepository
{
    public async Task<Guid> CreateAsync(Session session, CancellationToken cancellationToken = default)
    {
        session.Created = dateInfoService.CurrentDateTime;
        var created = await context.Sessions.AddAsync(session, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return created.Entity.SessionId;
    }

    public async Task UpdateAsync(Session session, CancellationToken cancellationToken = default)
    {
        var sessionDb = await context.Sessions.FirstOrDefaultAsync(e =>
            e.SessionId == session.SessionId, cancellationToken);

        if (sessionDb is null)
        {
            throw new NotFoundException(nameof(Session), session.SessionId);
        }

        sessionDb.Updated = dateInfoService.CurrentDateTime;
        sessionDb.RefreshToken = session.RefreshToken;

        context.Sessions.Update(sessionDb);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sessionDb = await context.Sessions.FirstOrDefaultAsync(e =>
            e.SessionId == id, cancellationToken);

        if (sessionDb is null)
        {
            throw new NotFoundException(nameof(Session), id);
        }

        context.Sessions.Remove(sessionDb);

        await context.SaveChangesAsync(cancellationToken);
    }
}
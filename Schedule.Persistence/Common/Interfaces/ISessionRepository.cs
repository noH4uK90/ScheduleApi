using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ISessionRepository
{
    public Task<Guid> CreateAsync(Session session, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Session session, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
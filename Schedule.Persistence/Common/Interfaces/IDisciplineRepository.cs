using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IDisciplineRepository
{
    public Task<Discipline> AddIfNotExists(string name, CancellationToken cancellationToken = default);

    public Task AddGroupDiscipline(int groupId, string name, CancellationToken cancellationToken = default);
}
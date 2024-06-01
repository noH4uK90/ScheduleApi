using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ITeacherRepository
{
    public Task<int> CreateAsync(Teacher teacher, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}
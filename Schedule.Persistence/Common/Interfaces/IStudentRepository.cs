using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IStudentRepository
{
    public Task<int> CreateAsync(Student student, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Student student, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);
}
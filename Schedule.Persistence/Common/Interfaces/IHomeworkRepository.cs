using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IHomeworkRepository
{
    public Task<int> CreateAsync(Homework homework, CancellationToken cancellationToken = default);

    public Task DeleteAsync(int homeworkId, CancellationToken cancellationToken = default);
}
using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ILessonTypeRepository
{
    public Task<LessonType> AddIfNotExists(string name, CancellationToken cancellationToken = default);
}
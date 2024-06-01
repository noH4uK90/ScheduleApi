using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ISubLessonRepository
{
    public Task<int> AddIfNotExists(ParsedScheduleSubItem subLesson, CancellationToken cancellationToken = default);

    public Task UpdateAsync(int subLessonId, ParsedScheduleSubItem subLesson,
        CancellationToken cancellationToken = default);

    public Task DeleteAsync(int subLessonId, CancellationToken cancellationToken = default);
}
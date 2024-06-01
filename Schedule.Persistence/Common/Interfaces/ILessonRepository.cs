using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ILessonRepository
{
    public Task AddAsync(int timetableId, ParsedScheduleItem lesson, string timeStart, string timeEnd, CancellationToken cancellationToken = default);

    public Task UpdateAsync(int timetableId, ParsedScheduleItem lesson, string timeStart, string timeEnd,
        CancellationToken cancellationToken = default);

    public Task DeleteAsync(int timetableId, CancellationToken cancellationToken = default);
}
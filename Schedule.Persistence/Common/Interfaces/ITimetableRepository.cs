using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ITimetableRepository
{
    public Task<Timetable> AddIfNotExists(Timetable timetable, CancellationToken cancellationToken = default);
}
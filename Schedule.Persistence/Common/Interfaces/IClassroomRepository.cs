using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IClassroomRepository
{
    public Task<Classroom> AddIfNotExists(string cabinet, CancellationToken cancellationToken = default);
}
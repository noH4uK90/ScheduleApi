using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface ITeacherFullNameRepository
{
    public Task<TeacherFullName> AddIfNotExists(string fullName, CancellationToken cancellationToken = default);
}
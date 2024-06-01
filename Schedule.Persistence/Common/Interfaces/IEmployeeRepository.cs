using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IEmployeeRepository
{
    public Task<int> CreateAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    public Task RestoreAsync(int id, CancellationToken cancellationToken = default);

    public Task UpdatePermissions(int id, int[] permissionIds, CancellationToken cancellationToken = default);
}
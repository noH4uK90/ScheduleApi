using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class EmployeeRepository(
    IScheduleDbContext context,
    IAccountRepository accountRepository)
    : IEmployeeRepository
{
    public async Task<int> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        return await context.WithTransactionAsync(async () =>
        {
            employee.Account.RoleId = (int)AccountRole.Employee;

            var accountId = await accountRepository.CreateAsync(employee.Account, cancellationToken);

            var created = await context.Employees.AddAsync(new Employee
            {
                AccountId = accountId,
            }, cancellationToken);

            var id = created.Entity.EmployeeId;

            // foreach (var employeePermission in employee.EmployeePermissions)
            // {
            //     await context.EmployeePermissions.AddAsync(new EmployeePermission
            //     {
            //         EmployeeId = id,
            //         PermissionId = employeePermission.PermissionId,
            //     }, cancellationToken);
            // }

            await context.SaveChangesAsync(cancellationToken);

            return id;
        }, cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var employeeDb = await context.Employees.FirstOrDefaultAsync(e =>
                e.EmployeeId == employee.EmployeeId, cancellationToken);

            if (employeeDb is null)
            {
                throw new NotFoundException(nameof(Employee), employee.EmployeeId);
            }

            employee.Account.AccountId = employeeDb.AccountId;

            await accountRepository.UpdateAsync(employee.Account, cancellationToken);

            // await context.EmployeePermissions
            //     .Where(e => e.EmployeeId == employeeDb.EmployeeId)
            //     .ExecuteDeleteAsync(cancellationToken);
            //
            // foreach (var employeePermission in employee.EmployeePermissions)
            // {
            //     await context.EmployeePermissions.AddAsync(new EmployeePermission
            //     {
            //         EmployeeId = employeeDb.EmployeeId,
            //         PermissionId = employeePermission.PermissionId,
            //     }, cancellationToken);
            // }

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        await accountRepository.DeleteAsync(employee.AccountId, cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmployeeId == id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        await accountRepository.RestoreAsync(employee.AccountId, cancellationToken);
    }

    public async Task UpdatePermissions(int id, int[] permissionIds, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var employee = await context.Employees.FirstOrDefaultAsync(e =>
                e.EmployeeId == id, cancellationToken);

            if (employee is null)
            {
                throw new NotFoundException(nameof(Employee), id);
            }

            // await context.EmployeePermissions
            //     .Where(e => e.EmployeeId == employee.EmployeeId)
            //     .ExecuteDeleteAsync(cancellationToken);
            //
            // foreach (var permissionId in permissionIds)
            // {
            //     await context.EmployeePermissions.AddAsync(new EmployeePermission
            //     {
            //         EmployeeId = id,
            //         PermissionId = permissionId,
            //     }, cancellationToken);
            // }

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}
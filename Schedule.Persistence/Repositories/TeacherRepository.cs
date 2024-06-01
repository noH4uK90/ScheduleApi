using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class TeacherRepository(
    IScheduleDbContext context,
    IAccountRepository accountRepository) : ITeacherRepository
{
    public async Task<int> CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        return await context.WithTransactionAsync(async () =>
        {
            teacher.Account.RoleId = (int)AccountRole.Teacher;

            var accountId = await accountRepository.CreateAsync(teacher.Account, cancellationToken);

            var created = await context.Teachers.AddAsync(new Teacher
            {
                AccountId = accountId,
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return created.Entity.TeacherId;
        }, cancellationToken);
    }

    public async Task UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var teacherDb = await context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TeacherId == teacher.TeacherId, cancellationToken);

            if (teacherDb is null)
            {
                throw new NotFoundException(nameof(Teacher), teacher.TeacherId);
            }

            teacher.Account.AccountId = teacherDb.AccountId;

            await accountRepository.UpdateAsync(teacher.Account, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var teacher = await context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TeacherId == id, cancellationToken);

        if (teacher is null)
        {
            throw new NotFoundException(nameof(Teacher), id);
        }

        await accountRepository.DeleteAsync(teacher.AccountId, cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var teacher = await context.Teachers
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TeacherId == id, cancellationToken);

        if (teacher is null)
        {
            throw new NotFoundException(nameof(Teacher), id);
        }

        await accountRepository.RestoreAsync(teacher.AccountId, cancellationToken);
    }
}
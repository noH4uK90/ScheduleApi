using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class StudentRepository(
    IScheduleDbContext context,
    IAccountRepository accountRepository) : IStudentRepository
{
    public async Task<int> CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        return await context.WithTransactionAsync(async () =>
        {
            student.Account.RoleId = (int)AccountRole.Student;

            var accountId = await accountRepository.CreateAsync(student.Account, cancellationToken);

            var created = await context.Students.AddAsync(new Student
            {
                AccountId = accountId,
                GroupId = student.GroupId
            }, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return created.Entity.StudentId;
        }, cancellationToken);
    }

    public async Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var studentDb = await context.Students.FirstOrDefaultAsync(e =>
                e.StudentId == student.StudentId, cancellationToken);

            if (studentDb is null)
            {
                throw new NotFoundException(nameof(Student), student.StudentId);
            }

            student.Account.AccountId = studentDb.AccountId;

            await accountRepository.UpdateAsync(student.Account, cancellationToken);
            
            studentDb.GroupId = student.GroupId;

            context.Students.Update(studentDb);
            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var student = await context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.StudentId == id, cancellationToken);

        if (student is null)
        {
            throw new NotFoundException(nameof(Student), id);
        }

        await accountRepository.DeleteAsync(student.AccountId, cancellationToken);
    }

    public async Task RestoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var student = await context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.StudentId == id, cancellationToken);

        if (student is null)
        {
            throw new NotFoundException(nameof(Student), id);
        }

        await accountRepository.RestoreAsync(student.AccountId, cancellationToken);
    }
}
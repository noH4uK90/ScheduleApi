using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Common.Interfaces;

namespace Schedule.Persistence.Repositories;

public class AccountRepository(
    IScheduleDbContext context,
    INameRepository nameRepository,
    ISurnameRepository surnameRepository,
    IMiddleNameRepository middleNameRepository,
    IPasswordHasherService passwordHasherService)
    : IAccountRepository
{
    public async Task<int> CreateAsync(Account account, CancellationToken cancellationToken = default)
    {
        return await context.WithTransactionAsync(async () =>
        {
            var searchByLogin = await FindByLoginAsync(account.Login, cancellationToken);

            if (searchByLogin is not null)
            {
                throw new AlreadyExistsException(account.Login);
            }

            var searchByEmail = await FindByEmailAsync(account.Email, cancellationToken);

            if (searchByEmail is not null)
            {
                throw new AlreadyExistsException(account.Email);
            }

            await nameRepository.AddIfNotExistAsync(account.Name, cancellationToken);
            await surnameRepository.AddIfNotExistAsync(account.Surname, cancellationToken);

            if (account.MiddleName is not null)
            {
                await middleNameRepository.AddIfNotExistAsync(account.MiddleName, cancellationToken);
            }

            account.PasswordHash = passwordHasherService.Hash(account.PasswordHash);

            var created = await context.Accounts.AddAsync(account, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return created.Entity.AccountId;
        }, cancellationToken);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var accountDb = await context.Accounts.FirstOrDefaultAsync(e =>
                e.AccountId == account.AccountId, cancellationToken);

            if (accountDb is null)
            {
                throw new NotFoundException(nameof(Account), account.AccountId);
            }

            var searchByLogin = await FindByLoginAsync(account.Login, cancellationToken);

            if (searchByLogin is not null)
            {
                throw new AlreadyExistsException(account.Login);
            }

            var searchByEmail = await FindByEmailAsync(account.Email, cancellationToken);

            if (searchByEmail is not null &&
                accountDb.Email != account.Email)
            {
                throw new AlreadyExistsException(account.Email);
            }

            await nameRepository.AddIfNotExistAsync(account.Name, cancellationToken);
            await surnameRepository.AddIfNotExistAsync(account.Surname, cancellationToken);

            if (account.MiddleName is not null)
            {
                await middleNameRepository.AddIfNotExistAsync(account.MiddleName, cancellationToken);
            }

            accountDb.Name = account.Name;
            accountDb.Surname = account.Surname;
            accountDb.MiddleName = account.MiddleName;
            accountDb.Email = account.Email;
            accountDb.RoleId = account.RoleId;

            context.Accounts.Update(accountDb);
            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteAsync(int accountId, CancellationToken cancellationToken = default)
    {
        await context.WithTransactionAsync(async () =>
        {
            var account = await context.Accounts.FirstOrDefaultAsync(e =>
                e.AccountId == accountId, cancellationToken);

            if (account is null)
            {
                throw new NotFoundException(nameof(Account), accountId);
            }

            //account.IsDeleted = true;

            context.Accounts.Update(account);

            await context.Sessions
                .Where(e => e.AccountId == account.AccountId)
                .ExecuteDeleteAsync(cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    public async Task RestoreAsync(int accountId, CancellationToken cancellationToken = default)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(e =>
            e.AccountId == accountId, cancellationToken);

        if (account is null)
        {
            throw new NotFoundException(nameof(Account), accountId);
        }

        //account.IsDeleted = false;

        context.Accounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Account?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Teachers)
            .Include(e => e.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }

    public async Task<Account?> FindByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return await context.Accounts
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Teachers)
            .Include(e => e.Students)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Login == login, cancellationToken);
    }

    public async Task ChangePasswordAsync(int accountId, string password, string newPassword, CancellationToken cancellationToken = default)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(e =>
            e.AccountId == accountId, cancellationToken);

        if (account is null)
        {
            throw new NotFoundException(nameof(Account), accountId);
        }

        if (!passwordHasherService.EnhancedHash(password, account.PasswordHash))
        {
            throw new IncorrectPasswordException();
        }

        account.PasswordHash = passwordHasherService.Hash(newPassword);

        context.Accounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RestorePasswordAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var account = await context.Accounts
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);

        if (account is null)
        {
            throw new NotFoundException(nameof(Account), email);
        }

        account.PasswordHash = passwordHasherService.Hash(password);
        context.Accounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);
    }
}
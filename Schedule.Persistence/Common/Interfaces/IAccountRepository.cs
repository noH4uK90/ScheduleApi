using Schedule.Core.Models;

namespace Schedule.Persistence.Common.Interfaces;

public interface IAccountRepository
{
    public Task<int> CreateAsync(Account account, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    public Task DeleteAsync(int accountId, CancellationToken cancellationToken = default);
    public Task RestoreAsync(int accountId, CancellationToken cancellationToken = default);

    public Task<Account?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<Account?> FindByLoginAsync(string login, CancellationToken cancellationToken = default);

    public Task ChangePasswordAsync(int accountId, string password, string newPassword, CancellationToken cancellationToken = default);
    public Task RestorePasswordAsync(string email, string password, CancellationToken cancellationToken = default);
}
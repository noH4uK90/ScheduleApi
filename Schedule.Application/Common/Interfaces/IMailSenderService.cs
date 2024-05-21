using Schedule.Core.Models;

namespace Schedule.Application.Common.Interfaces;

public interface IMailSenderService
{
    public Task SendAsync(Letter letter, CancellationToken cancellationToken = default);
}
namespace Schedule.Persistence.Common.Interfaces;

public interface IMiddleNameRepository
{
    public Task AddIfNotExistAsync(string middleName, CancellationToken cancellationToken = default);
}
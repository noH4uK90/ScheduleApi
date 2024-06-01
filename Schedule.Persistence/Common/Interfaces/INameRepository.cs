namespace Schedule.Persistence.Common.Interfaces;

public interface INameRepository
{
    public Task AddIfNotExistAsync(string name, CancellationToken cancellationToken = default);
}
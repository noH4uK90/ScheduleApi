namespace Schedule.Persistence.Common.Interfaces;

public interface ISurnameRepository
{
    public Task AddIfNotExistAsync(string surname, CancellationToken cancellationToken = default);
}
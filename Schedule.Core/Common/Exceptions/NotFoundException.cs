namespace Schedule.Core.Common.Exceptions;

public sealed class NotFoundException : ScheduleException
{
    public NotFoundException(string name)
        : base($"Entity \"{name}\" was not found.")
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" with key \"{key}\" was not found.")
    {
    }
}
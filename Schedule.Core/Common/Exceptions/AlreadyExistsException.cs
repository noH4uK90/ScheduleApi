namespace Schedule.Core.Common.Exceptions;

public sealed class AlreadyExistsException : ScheduleException
{
    public AlreadyExistsException(string value)
        : base($"Entity already exists. Value: {value}")
    {
        Value = value;
    }
    
    public string Value { get; }
}
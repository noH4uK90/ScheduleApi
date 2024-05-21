namespace Schedule.Core.Common.Exceptions;

public class ScheduleException : Exception
{
    public ScheduleException()
    {
    }

    public ScheduleException(string message)
        : base(message)
    {
    }

    public ScheduleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
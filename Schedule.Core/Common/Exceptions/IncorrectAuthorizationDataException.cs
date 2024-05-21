namespace Schedule.Core.Common.Exceptions;

public sealed class IncorrectAuthorizationDataException : ScheduleException
{
    public IncorrectAuthorizationDataException()
        : base("Incrorrect login or password.")
    {
    }
}
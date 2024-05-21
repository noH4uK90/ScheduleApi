namespace Schedule.Core.Common.Exceptions;

public class IncorrectPasswordException : ScheduleException
{
    public IncorrectPasswordException() 
        : base("Incorrect password")
    {
    }
}
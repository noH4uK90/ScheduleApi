namespace Schedule.Core.Common.Exceptions;

public sealed class DeletedException(string message) : ScheduleException(message);